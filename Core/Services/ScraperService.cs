using Core.Entities;
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
using System.Threading.Tasks;

namespace Core.Services
{
    public class ScraperService : IScraperService
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private  readonly RacingPostSettings _racingPostConfig;
        private  readonly IConfigurationService _configService;

        public ScraperService(IConfigurationService configService)
        {
            _configService = configService;
            _racingPostConfig = _configService.GetRacingPostSettings();
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

        public async Task<RaceModel> RetrieveRacesForEvent(EventEntity eventEntity)
        {
            var result = new RaceModel();
            result.RaceEntities = new List<RaceEntity>();
            //Retrieve the event info from the DB
            try
            {
                //Build the URL
                var url = $"{_racingPostConfig.BaseUrl + eventEntity.meeting_url}";

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
                        weather = result.Weather
                    };
                    race = await ExtractRaceInfo(div, race);

                    result.RaceEntities.Add(race);
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to retrieve races for event {eventEntity.event_id} ... {ex.Message}");
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
            raceEntity.race_class =Extractor.ExtractIntsFromString(race_class);

            return raceEntity;

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
    }
}
