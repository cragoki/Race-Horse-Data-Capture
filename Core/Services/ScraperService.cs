using Core.Entities;
using Core.Enums;
using Core.Helpers;
using Core.Interfaces.Data.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Models.GetRace;
using Core.Models.Settings;
using HtmlAgilityPack;
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

        public ScraperService(IConfigurationService configService, IHorseRepository horseRepository)
        {
            _configService = configService;
            _racingPostConfig = _configService.GetRacingPostSettings();
            _horseRepository = horseRepository;
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
                var getTableRows = await ExtractJson(page, "cardsMatrix\":", "}]}]");

                //Convert Json to model
                result = JsonConvert.DeserializeObject<DailyRaces>(getTableRows + "}");
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

        public async Task<RaceModel> RetrieveRacesForEvent(EventEntity eventEntity)
        {
            var result = new RaceModel();
            result.RaceEntities = new List<RaceEntity>();
            //Retrieve the event info from the DB
            try
            {
                //Build the URL
                var url = $"{_racingPostConfig.BaseUrl + eventEntity.meeting_url}";
                Thread.Sleep(2500);
                //Get the raw HTML
                var page = await CallUrl(url);
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(page);

                result.CourseUrl = await ExtractCourseUrl(htmlDoc);
                result.Weather = await ExtractWeather(htmlDoc);

                var divs = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class,'RC-meetingDay__race')]").Where(x => x.Attributes.Contains("data-diffusion-racetime"));

                foreach (HtmlNode div in divs)
                {
                    var raceTime = div.Attributes["data-diffusion-racetime"].Value;
                    var rpRaceId = div.Attributes["data-diffusion-race-id"].Value;
                    var raceUrl = $"{eventEntity.meeting_url}/{rpRaceId}";

                    var race = new RaceEntity()
                    {
                        race_time = raceTime,
                        rp_race_id = Int32.Parse(rpRaceId),
                        race_url = raceUrl,
                        event_id = eventEntity.event_id,
                        weather = result.Weather,
                        completed = false
                    };
                    race = await ExtractRaceInfo(div, race);

                    result.RaceEntities.Add(race);

                    //race horse extraction

                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to retrieve races for event {eventEntity.event_id} ... {ex.Message}");
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

                foreach (var horse in horseContainers)
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

                    if (toUpdate != null) 
                    {
                        toUpdate.position = Int32.Parse(formattedPos);
                        toUpdate.description = comment.Replace(" ", "");
                        toUpdate.finished = true;

                        _horseRepository.UpdateRaceHorse(toUpdate);

                        result.Add(toUpdate);
                    }

                }

            }
            catch (Exception ex) 
            {
                Logger.Error($"Failed to retrieve results for race {race.race_id} ... {ex.Message}");
            }

            return result;
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

            raceEntity.ages = nodeDescription.SelectSingleNode("span[contains(@data-test-selector,'RC-meetingDay__raceAgesAllowed')]")?.InnerText.Replace(" ", "") ?? "";
            raceEntity.going = nodeDescription.SelectSingleNode("span[contains(@data-test-selector,'RC-meetingDay__raceGoingType')]")?.InnerText ?? "";
            raceEntity.stalls = nodeDescription.SelectSingleNode("span[contains(@data-test-selector,'RC-meetingDay__raceStalls')]")?.InnerText ?? "";
            raceEntity.no_of_horses = Extractor.ExtractIntsFromString(no_of_horses);
            raceEntity.distance = nodeDescription.SelectSingleNode("span[contains(@data-test-selector,'RC-meetingDay__raceDistance')]")?.InnerText.Replace(" ", "") ?? "";
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
                        _horseRepository.AddArchiveHorse(horseArchiveRPR);
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
                        _horseRepository.AddArchiveHorse(horseArchiveTS);
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

        private async Task<string> ExtractJson(string strSource, string strStart, string strEnd) 
        {
            var result = "";

            try
            {
                if (strSource.Contains(strStart) && strSource.Contains(strEnd))
                {
                    int Start, End;
                    Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                    End = strSource.IndexOf(strEnd, Start);
                    return strSource.Substring(Start, End + strEnd.Length - Start);
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
    }
}
