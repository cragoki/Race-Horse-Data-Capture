using Core.Interfaces.Services;
using Core.Models.GetRace;
using Core.Models.Settings;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
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

        public void RetrieveRacesForEvent(int eventId)
        {
            //Retrieve the event info from the DB
            try
            {

                //We will need to fetch and return a race object here...

            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to retrieve races for event {eventId}");
            }
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
