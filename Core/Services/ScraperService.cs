using Core.Entities;
using Core.Enums;
using Core.Helpers;
using Core.Interfaces.Data.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Models.GetRace;
using Core.Models.RP.GetRaceNew;
using Core.Models.Settings;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ScraperService : IScraperService
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly RacingPostSettings _racingPostConfig;
        private readonly IConfigurationService _configService;
        private readonly IHorseRepository _horseRepository;
        private readonly IMappingTableRepository _mappingTableRepository;

        public ScraperService(IConfigurationService configService, IHorseRepository horseRepository, IMappingTableRepository mappingTableRepository)
        {
            _configService = configService;
            _racingPostConfig = _configService.GetRacingPostSettings();
            _horseRepository = horseRepository;
            _mappingTableRepository = mappingTableRepository;
        }

        public async Task<DailyRaces> RetrieveTodaysEvents()
        {
            var result = new DailyRaces();
            //Retrieve the events for today
            try
            {
                //Build the URL
                var url = $"{_racingPostConfig.BaseUrl}";

                //Get the raw HTML
                var page = await CallUrl(url);
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(page);

                //Now we have the page we must parse the table into individual elements.
                var getTableRows = await ExtractJson(page, "byDate\":", "\"dates\":[", 0, 1);

                //For new Structure, cheers racing post for ruining my day and changing stuff on your UI :(
                var startString = DateTime.Now.Date.ToString("yyyy-MM-dd");
                var endString = DateTime.Now.Date.AddDays(1).ToString("yyyy-MM-dd");
                getTableRows = await ExtractJson(getTableRows, startString, endString, 2, 2);

                //New Objects for RP restructure

                result = await ConvertNewRPStructureToOld(getTableRows);
            }
            catch (Exception ex)
            {
                Logger.Error($"!!! Failed to retrieve todays events.  Terminating process. {ex.Message} !!!");
                throw new Exception(ex.Message);
            }

            return result;
        }

        public async Task<DailyRaces> RetrieveBacklogEvents(DateTime date)
        {
            var result = new DailyRaces();
            //Retrieve the events for today
            try
            {
                var dateFormatted = date.Date.ToString("yyyy-MM-dd");

                //Build the URL
                var url = $"{_racingPostConfig.BacklogUrl}{dateFormatted}";

                //Get the raw HTML
                var page = await CallUrl(url);
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(page);

                var resultDivs = htmlDoc.DocumentNode.SelectNodes("//section[contains(@class,'rp-raceCourse__meetingContainer')]");

                foreach (var section in resultDivs) 
                {
                    var toAdd = new Course();

                    //TO ADD: RACING POST COURSE_ID = section.attribute = "data-course-id",
                    //Course Name = section.attribute = data-diffusion-coursename
                    //Get Course Name + check if it exists. if it does, use existing course info.

                    //Seems to be split into two kind of divs from here. The Meeting details: div class = "rp-raceCourse__panel__details"
                    //Expect 1
                    //Data: "Going", "Weather", "Total Runners", "Stalls"
                    //and the races are split into their own divs: div class = "rp-raceCourse__panel__container"


                    toAdd.Abandoned = false;
                    toAdd.CountryCode = "";

                    result.Courses.Add(toAdd);
                }

            }
            catch (Exception ex)
            {
                Logger.Error($"!!! Failed to retrieve todays events.  Terminating process. {ex.Message} !!!");
                throw new Exception(ex.Message);
            }

            return result;
        }

        public async Task<List<RaceHorseModel>> RetrieveHorseDetailsForRace(RaceEntity race)
        {
            var result = new List<RaceHorseModel>();

            try
            {
                //Build the URL
                var url = $"{_racingPostConfig.BaseUrl + race.race_url}";
                Thread.Sleep(2500);
                //Get the raw HTML
                var page = await CallUrl(url);
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(page);

                var horseContainers = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class,'RC-runnerCardWrapper')]");

                foreach (var horse in horseContainers.ToList())
                {
                    var raceHorse = new RaceHorseEntity()
                    {
                        race_id = race.race_id,
                        finished = false,
                    };
                    var raceHorseDetails = await ExtractRaceHorseData(horse, raceHorse, result);

                    if (raceHorseDetails.RaceHorse != null && raceHorseDetails.Horse != null)
                    {
                        result.Add(raceHorseDetails);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to retrieve races for race {race.race_id} ... {ex.Message}");
                throw new Exception(ex.Message);
            }


            return result;
        }

        public async Task<List<RaceHorseEntity>> GetResultsForRace(RaceEntity race, List<RaceHorseEntity> raceHorses) 
        {
            var result = new List<RaceHorseEntity>();

            try
            {
                //Build the URL
                var resultUrl = race.race_url.Replace("racecards", "results");
                var url = $"{_racingPostConfig.BaseUrl + resultUrl}";
                Thread.Sleep(5000);
                //Get the raw HTML
                var page = await CallUrl(url);
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(page);

                var resultDivs = htmlDoc.DocumentNode.SelectNodes("//tr[contains(@class,'rp-horseTable__mainRow')]");
                var commentDivs = htmlDoc.DocumentNode.SelectNodes("//tr[contains(@class,'rp-horseTable__commentRow')]");

                for (int i = 0; i < resultDivs.Count; i++) 
                {
                    //Horse
                    //div class rp-horseTable__horse
                    var horseDiv = resultDivs[i].SelectSingleNode(".//div[contains(@class, 'rp-horseTable__horse')]");
                    //a class rp-horseTable__horse__name => href get url and parse into ID
                    var rpHorseurl = horseDiv.SelectSingleNode(".//a[contains(@class,'rp-horseTable__horse__name')]")?.Attributes["href"].Value ?? "";
                    var horseId = await ExtractHorseIdFromUrl(rpHorseurl);


                    //Position
                    //div class rp-horseTable__pos__numWrapper
                    var positionDiv = resultDivs[i].SelectSingleNode(".//div[contains(@class, 'rp-horseTable__pos__numWrapper')]");
                    //span class rp-horseTable__pos__number inner text
                    var position = positionDiv.SelectSingleNode(".//span[contains(@class, 'rp-horseTable__pos__number')]")?.InnerText.Replace(" ", "");
                    string formattedPos = "";
                    var index = position.IndexOf("(");
                    if (index == -1)
                    {
                        formattedPos = Regex.Match(position, @"\d+").Value;
                    }
                    else 
                    {
                         formattedPos = Regex.Match(position, @"\d+").Value;
                    }
                    //Comment
                    //tr class rp-horseTable__commentRow
                    var comment = commentDivs[i].SelectSingleNode("td").InnerText;

                    var horse = _horseRepository.GetHorseByRpId(horseId);
                    var toUpdate = raceHorses.Where(x => x.horse_id == horse.horse_id).FirstOrDefault();

                    try
                    {

                        if (toUpdate != null)
                        {
                            if (String.IsNullOrEmpty(formattedPos))
                            {
                                toUpdate.position = 0;

                                if (position.Contains("PU"))
                                {
                                    toUpdate.description = "PU";
                                }
                                else if (position.Contains("UR"))
                                {
                                    toUpdate.description = "UR";
                                }
                                else if (position.Contains("F"))
                                {
                                    toUpdate.description = "F";
                                }
                                else if (position.Contains("BD"))
                                {
                                    toUpdate.description = "BD";
                                }
                                else if (position.Contains("RO"))
                                {
                                    toUpdate.description = "RO";
                                }
                                else
                                {
                                    toUpdate.position = Int32.Parse(formattedPos);

                                    if (toUpdate.position == 0)
                                    {
                                        toUpdate.description = "NR";
                                    }
                                    else
                                    {
                                        toUpdate.description = "";
                                    }
                                }

                                toUpdate.finished = true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        toUpdate.position = -1;
                        toUpdate.description = ex.Message + " --- " + formattedPos + "---" + position;
                        toUpdate.finished = false;
                    }
                    if (toUpdate.position == 0 && String.IsNullOrEmpty(toUpdate.description))
                    {
                        toUpdate.position = -1;
                        toUpdate.description = "Unknown Error";
                    }
                    result.Add(toUpdate);
                    _horseRepository.UpdateRaceHorse(toUpdate);

                }

            }
            catch (Exception ex) 
            {
                Logger.Error($"Failed to retrieve results for race {race.race_id} ... {ex.Message}");
            }

            return result;
        }

        public async Task<RaceHorseEntity> GetResultsForRaceHorse(RaceHorseEntity raceHorse)
        {
            try
            {
                //Build the URL
                var resultUrl = raceHorse.Race.race_url.Replace("racecards", "results");
                var url = $"{_racingPostConfig.BaseUrl + resultUrl}";
                Thread.Sleep(5000);
                //Get the raw HTML
                var page = await CallUrl(url);
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(page);

                var resultDivs = htmlDoc.DocumentNode.SelectNodes("//tr[contains(@class,'rp-horseTable__mainRow')]");
                var commentDivs = htmlDoc.DocumentNode.SelectNodes("//tr[contains(@class,'rp-horseTable__commentRow')]");
                
                for (int i = 0; i < resultDivs.Count; i++)
                {
                    //Horse
                    //div class rp-horseTable__horse
                    var horseDiv = resultDivs[i].SelectSingleNode(".//div[contains(@class, 'rp-horseTable__horse')]");
                    //a class rp-horseTable__horse__name => href get url and parse into ID
                    var rpHorseurl = horseDiv.SelectSingleNode(".//a[contains(@class,'rp-horseTable__horse__name')]")?.Attributes["href"].Value ?? "";
                    var horseId = await ExtractHorseIdFromUrl(rpHorseurl);


                    //Position
                    //div class rp-horseTable__pos__numWrapper
                    var positionDiv = resultDivs[i].SelectSingleNode(".//div[contains(@class, 'rp-horseTable__pos__numWrapper')]");
                    //span class rp-horseTable__pos__number inner text
                    var position = positionDiv.SelectSingleNode(".//span[contains(@class, 'rp-horseTable__pos__number')]")?.InnerText.Replace(" ", "");
                    string formattedPos = "";
                    var index = position.IndexOf("(");
                    if (index == -1)
                    {
                        formattedPos = Regex.Match(position, @"\d+").Value;
                    }
                    else
                    {
                        formattedPos = Regex.Match(position, @"\d+").Value;
                    }
                    //Comment
                    //tr class rp-horseTable__commentRow
                    var comment = commentDivs[i].SelectSingleNode("td").InnerText;

                    var horse = _horseRepository.GetHorseByRpId(horseId);

                    if (horse != null && horse.horse_id == raceHorse.horse_id) 
                    {
                        try
                        {
                            if (String.IsNullOrEmpty(formattedPos))
                            {
                                raceHorse.position = 0;

                                if (position.Contains("PU"))
                                {
                                    raceHorse.description = "PU";
                                }
                                else if (position.Contains("UR"))
                                {
                                    raceHorse.description = "UR";
                                }
                                else if (position.Contains("F"))
                                {
                                    raceHorse.description = "F";
                                }
                                else if (position.Contains("BD"))
                                {
                                    raceHorse.description = "BD";
                                }
                                else if (position.Contains("RO"))
                                {
                                    raceHorse.description = "RO";
                                }
                            }
                            else 
                            {
                                raceHorse.position = Int32.Parse(formattedPos);
                                if (raceHorse.position == 0)
                                {
                                    raceHorse.description = "NR";
                                }
                                raceHorse.finished = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            raceHorse.position = -1;
                            raceHorse.description = ex.Message;
                            raceHorse.finished = false;
                            //Could log errors to a seperate table to review and maybe manually input?
                        }

                        if (raceHorse.position == 0 && String.IsNullOrEmpty(raceHorse.description)) 
                        {
                            raceHorse.position = -1;
                            raceHorse.description ="Unknown Error";
                        }
                        return raceHorse;
                    }

                }

                raceHorse.position = 0;
                raceHorse.description = "NR";
                raceHorse.finished = false;
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to retrieve results for race horse {raceHorse.race_horse_id} ... {ex.Message}");
                raceHorse.position = -1;
                raceHorse.description = ex.Message;
                raceHorse.finished = false;
            }

            return raceHorse;
        }

        private async Task<string> ExtractCourseUrl(HtmlDocument htmlDoc)
        {
            //Get the race course h2 to extract the course URL
            string raceCourse = htmlDoc.DocumentNode
            .SelectSingleNode("//h2[contains(@class,'RC-meetingDay__titleName')]").InnerHtml;
            HtmlDocument courseUrlDoc = new HtmlDocument();
            courseUrlDoc.LoadHtml(raceCourse);

            //This should extract the course URL
            string courseUrl = courseUrlDoc.DocumentNode
            .SelectSingleNode("//a[contains(@class,'ui-link')]")
            .Attributes["href"].Value;

            return courseUrl;
        }

        private async Task<string> ExtractWeather(HtmlDocument htmlDoc)
        {
            //Get the race course h2 to extract the course URL
            string raceCourse = htmlDoc.DocumentNode
            .SelectSingleNode("//div[contains(@class,'RC-meetingDay__description')]").InnerHtml;
            HtmlDocument courseUrlDoc = new HtmlDocument();
            courseUrlDoc.LoadHtml(raceCourse);

            //This should extract the course URL
            var weather = courseUrlDoc.DocumentNode
            .SelectNodes("//span");

            return weather[1].InnerText;
        }

        private async Task<RaceEntity> ExtractRaceInfo(HtmlNode htmlDoc, RaceEntity raceEntity)
        {
            //RC-meetingDay__raceDescription
            //div class = RC-cardHeader
            var nodeDescription = htmlDoc.SelectSingleNode("div[contains(@class, 'RC-meetingDay__raceHeader')]").SelectSingleNode("div[contains(@class, 'RC-meetingDay__raceDescription')]");
            var no_of_horses = nodeDescription.SelectSingleNode("span[contains(@data-test-selector, 'RC-meetingDay__raceRunnersNo')]")?.InnerText ?? "0";
            var race_class = nodeDescription.SelectSingleNode("span[contains(@data-test-selector,'RC-meetingDay__raceClass')]")?.InnerText ?? "0";

            raceEntity.ages = _mappingTableRepository.AddOrReturnAgeType(nodeDescription.SelectSingleNode("span[contains(@data-test-selector,'RC-meetingDay__raceAgesAllowed')]")?.InnerText.Replace(" ", "") ?? "");
            raceEntity.going = _mappingTableRepository.AddOrReturnGoingType(nodeDescription.SelectSingleNode("span[contains(@data-test-selector,'RC-meetingDay__raceGoingType')]")?.InnerText ?? "");
            raceEntity.stalls = _mappingTableRepository.AddOrReturnStallsType(nodeDescription.SelectSingleNode("span[contains(@data-test-selector,'RC-meetingDay__raceStalls')]")?.InnerText ?? "");
            raceEntity.no_of_horses = Extractor.ExtractIntsFromString(no_of_horses);
            raceEntity.distance = _mappingTableRepository.AddOrReturnDistanceType(nodeDescription.SelectSingleNode("span[contains(@data-test-selector,'RC-meetingDay__raceDistance')]")?.InnerText.Replace(" ", "") ?? "");
            raceEntity.description = nodeDescription.SelectSingleNode("a[contains(@class,'RC-meetingDay__raceTitle')]")?.InnerText ?? "";
            raceEntity.race_class = Extractor.ExtractIntsFromString(race_class);

            return raceEntity;

        }

        private async Task<RaceHorseModel> ExtractRaceHorseData(HtmlNode htmlDoc, RaceHorseEntity raceHorse, List<RaceHorseModel> existing)
        {
            var result = new RaceHorseModel();

            var horseName = htmlDoc.SelectSingleNode(".//a[contains(@class, 'RC-runnerName')]").InnerText.Replace(" ", "");

            if (!existing.Any(x => x.Horse.horse_name == horseName))
            {
                var horseUrl = htmlDoc.SelectSingleNode(".//a[contains(@class, 'RC-runnerName')]").Attributes["href"].Value; // remove all after #
                var horseUrlIndex = horseUrl.IndexOf("#");
                horseUrl = horseUrl.Remove(horseUrlIndex);

                var rpr = htmlDoc.SelectSingleNode(".//span[contains(@class, 'runnerRpr')]").InnerText.Replace(" ", "");
                var ts = htmlDoc.SelectSingleNode(".//span[contains(@class, 'RC-runnerTs')]").InnerText.Replace(" ", "");
                int rprInt = 0;
                int tsInt = 0;
                Int32.TryParse(RegexHelper.RemoveWhitespace(rpr), out rprInt);
                Int32.TryParse(RegexHelper.RemoveWhitespace(ts), out tsInt);

                int rpHorseId = await ExtractHorseIdFromUrl(horseUrl);

                //Get Race Horse data
                //Jockey

                //Get div with class RC-runnerInfo_jockey
                var jockeyContainer = htmlDoc.SelectSingleNode(".//div[contains(@class, 'RC-runnerInfo_jockey')]");
                //Look for a with class RC-runnerInfo__name => Inner Text = name, href = url
                var jockey = new JockeyEntity()
                {
                    jockey_name = jockeyContainer.SelectSingleNode(".//a[contains(@class, 'RC-runnerInfo__name')]")?.InnerText.Replace(" ","") ?? "",
                    jockey_url = jockeyContainer.SelectSingleNode(".//a[contains(@class, 'RC-runnerInfo__name')]")?.Attributes["href"].Value ?? ""
                };

                //Trainer
                //Get div with class RC-runnerInfo_trainer
                //Look for a with class RC-runnerInfo__name => Inner Text = name, href = url
                var trainerContainer = htmlDoc.SelectSingleNode(".//div[contains(@class, 'RC-runnerInfo_trainer')]");

                var trainer = new TrainerEntity()
                {
                    trainer_name = trainerContainer.SelectSingleNode(".//a[contains(@class, 'RC-runnerInfo__name')]")?.InnerText.Replace(" ", "") ?? "",
                    trainer_url = trainerContainer.SelectSingleNode(".//a[contains(@class, 'RC-runnerInfo__name')]")?.Attributes["href"].Value ?? ""
                };

                //Horse
                var horse = new HorseEntity()
                {
                    horse_name = horseName,//horseName,
                    horse_url = horseUrl,
                    rpr = rprInt,// span with this class runnerRpr
                    top_speed = tsInt,// span with this class RC-runnerTs
                    //dob = //await ExtractHorseData(horseUrl),
                    rp_horse_id = rpHorseId
                };

                //Fetch the remaining race horse info
                var horseWeightS = htmlDoc.SelectSingleNode(".//span[contains(@class, 'RC-runnerWgt__carried_st')]").InnerText.Replace(" ", "") ?? "";
                var horseWeightL = htmlDoc.SelectSingleNode(".//span[contains(@class, 'RC-runnerWgt__carried_lb')]").InnerText.Replace(" ", "") ?? "";
                var horseWeight = $"{horseWeightS}.{horseWeightL}";
                var age = Int32.Parse(htmlDoc.SelectSingleNode(".//span[contains(@class, 'RC-runnerAge')]").InnerText ?? "");

                var identity = await AddOrUpdateHorseData(horse, jockey, trainer);

                raceHorse.jockey_id = identity.JockeyId;
                raceHorse.trainer_id = identity.TrainerId;
                raceHorse.horse_id = identity.HorseId;
                horse.horse_id = identity.HorseId;
                raceHorse.weight = horseWeight;
                raceHorse.age = age;


                result = new RaceHorseModel()
                {
                    RaceHorse = raceHorse,
                    Horse = horse
                };
            }

            return result;
        }

        private async Task<AddUpdateHorseModel> AddOrUpdateHorseData(HorseEntity horse, JockeyEntity jockey, TrainerEntity trainer)
        {
            var result = new AddUpdateHorseModel();
            var existingHorse = _horseRepository.GetHorseByRpId(horse.rp_horse_id);
            var existingJockey = _horseRepository.GetJockeyByName(jockey.jockey_name);
            var existingTrainer = _horseRepository.GetTrainerByName(trainer.trainer_name);

            if (existingHorse != null)
            {
                result.HorseId = existingHorse.horse_id;

                if (horse.rpr != existingHorse.rpr || horse.top_speed != existingHorse.top_speed)
                {
                    if (horse.rpr != existingHorse.rpr)
                    {
                        var horseArchiveRPR = new HorseArchiveEntity()
                        {
                            horse_id = existingHorse.horse_id,
                            field_changed = HorseFieldEnum.rpr.ToString(),
                            old_value = existingHorse.rpr.ToString(),
                            new_value = horse.rpr.ToString(),
                            date = DateTime.Now
                        };
                        existingHorse.rpr = horse.rpr;
                        existingHorse.Archive.Add(horseArchiveRPR);
                    }
                    if (horse.top_speed != existingHorse.top_speed)
                    {
                        var horseArchiveTS = new HorseArchiveEntity()
                        {
                            horse_id = existingHorse.horse_id,
                            field_changed = HorseFieldEnum.ts.ToString(),
                            old_value = existingHorse.top_speed.ToString(),
                            new_value = horse.top_speed.ToString(),
                            date = DateTime.Now
                        };
                        existingHorse.top_speed = horse.top_speed;
                        existingHorse.Archive.Add(horseArchiveTS);
                    }

                    _horseRepository.UpdateHorse(existingHorse);

                }
            }
            else 
            {
                result.HorseId = _horseRepository.AddHorse(horse);
            }

            if (existingJockey != null)
            {
                result.JockeyId = existingJockey.jockey_id;
            }
            else 
            {
                result.JockeyId = _horseRepository.AddJockey(jockey);
            }

            if (existingTrainer != null)
            {
                result.TrainerId = existingTrainer.trainer_id;
            }
            else 
            {
                result.TrainerId = _horseRepository.AddTrainer(trainer);
            }

            return result;
        }

        private async Task<DateTime> ExtractHorseData(string horseUrl)
        {
            //Build the URL
            var url = $"{_racingPostConfig.BaseUrl + horseUrl}";

            //Get the raw HTML
            var page = await CallUrl(url);
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(page);

            var horseContainer = htmlDoc.DocumentNode.SelectSingleNode("//div[contains(@class,'hp-details')]");

            return DateTime.Now;
        }


        private async Task<string> CallUrl(string fullUrl)
        {
            try
            {
                //Get the HTML from a specific page
                var handler = new HttpClientHandler()
                {
                    AllowAutoRedirect = false
                };

                HttpClient client = new HttpClient(handler);

                //If you get HTTPS handshake errors, it’s likely because you are not using the right cryptographic library.
                ////The below statement forces the connection to use the TLS 1.3 library so that an HTTPS handshake can be established.
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
                client.DefaultRequestHeaders.Accept.Clear();
                var response = await client.GetStringAsync(fullUrl);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<string> ExtractJson(string strSource, string strStart, string strEnd, int trimStart, int trimEnd) 
        {
            var result = "";

            try
            {
                if (strSource.Contains(strStart) && strSource.Contains(strEnd))
                {
                    int Start, End;
                    Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                    End = strSource.IndexOf(strEnd, Start);
                    result = strSource.Substring(Start, End + strEnd.Length - Start);
                    result = result.Substring(0, result.Length - (strEnd.Length + trimEnd));
                    result = result.Substring(trimStart);
                }
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }

            return result;
        }

        private async Task<int> ExtractHorseIdFromUrl(string horseUrl) 
        {
            var rpHorseIdSubstr = horseUrl.Substring(15);
            var horseIdIndex = rpHorseIdSubstr.IndexOf("/");
            return Int32.Parse(rpHorseIdSubstr.Remove(horseIdIndex));
        }

        private async Task<DailyRaces> ConvertNewRPStructureToOld(string page) 
        {
            var result = new DailyRaces();
            result.Courses = new List<Course>();
            try
            {
                var events = new List<SubMeeting>();

                //Convert Json to model
                var meetingAndRaceIds = JsonConvert.DeserializeObject<GetRaceBase>(page);

                foreach (var eventId in meetingAndRaceIds.Meetings.EventIds)
                {
                    var eventData = await ExtractJson(page, $"\"{eventId.ToString()}\":", "},", 0, 0);
                    eventData = FormatJsonString(eventData);
                    events.Add(JsonConvert.DeserializeObject<SubMeeting>(eventData));
                }

                foreach (var even in events) 
                {
                    var races = new List<NewRaceModel>();

                    foreach (var raceId in even.RaceIds) 
                    {
                        var raceData = await ExtractJson(page, $"\"{raceId.ToString()}\":", ",\"startScheduledDatetimeUTC\"", 0, 0);
                        raceData = FormatJsonString(raceData);
                        races.Add(JsonConvert.DeserializeObject<NewRaceModel>(raceData));
                    }

                    var racesToAdd = new List<Race>();

                    foreach (var race in races) 
                    {
                        racesToAdd.Add(new Race()
                        {
                            Id = race.RPRaceId,
                            RaceURL = race.RaceUrl,
                            Date = race.Date,
                            Abandoned = race.Abandoned,
                            HasRaceOffers = false,
                            IsNextRace = false,
                            IsThirthyMinToNextRace = false,
                            Result = false,
                            Runners = race.Runners,
                            Started = "",
                            Time = race.Time,
                            Title = race.Title,
                            RaceClass = String.IsNullOrEmpty(race.Class) ? 0 : Int32.Parse(race.Class),
                            Distance = race.Distance,
                            Ages = race.Ages,
                            Weather = even.Weather,
                            Going = race.Going
                        });
                    }

                    result.Courses.Add(new Course()
                    {
                        Id = even.RacingPostId,
                        Name = even.Name,
                        Races = racesToAdd,
                        Abandoned = even.IsAbandoned,
                        AllWeather = false,
                        Colour = 0,
                        CountryCode = even.CountryCode,
                        HashName = even.HashName,
                        SurfaceType = even.SurfaceType, //This will be taken from Race Now
                        meetingTypeCode = even.MeetingType,
                        MeetingUrl = "" //No longer have this :(
                    });
                }

               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }

        private string FormatJsonString(string jsonString) 
        {
            if (jsonString.Last().ToString() != "}")
            {
                jsonString = jsonString + "}";
            }
            if (jsonString.First().ToString() != "{")
            {
                jsonString = "{" + jsonString;
            }
            return jsonString;
        }
    }
}
