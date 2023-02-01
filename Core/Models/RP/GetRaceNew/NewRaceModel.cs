using Newtonsoft.Json;

namespace Core.Models.RP.GetRaceNew
{
    public class NewRaceModel
    {
        [JsonProperty("raceId")]
        public int RPRaceId { get; set; }
        [JsonProperty("isAbandoned")]
        public bool Abandoned { get; set; }
        [JsonProperty("raceTitle")]
        public string Title { get; set; }

        [JsonProperty("startTime")]
        public string Time { get; set; }

        [JsonProperty("numberOfRunners")]
        public int? Runners { get; set; }

        [JsonProperty("ukDateFormat")]
        public string Date { get; set; }
        [JsonProperty("meetingId")]
        public int MeetingId { get; set; }

        [JsonProperty("hybridRaceUrl")]
        public string RaceUrl { get; set; }
        [JsonProperty("raceClass")]
        public string Class { get; set; }
        [JsonProperty("displayDistance")]
        public string Distance { get; set; }
        [JsonProperty("ageRestriction")]
        public string Ages { get; set; }
        [JsonProperty("going")]
        public string Going { get; set; }

        //"isUpcoming":false,
        //"raceTitle":"Weatherbys Cheltenham Festival Guide Handicap Chase",
        //"startTime":"15:00",
        //"startDateTime":"2023-02-01 15:00",
        //"ukDateFormat":"2023-02-01",
        //"diffusionMeetingName":"AYR",
        //"raceUrl":"/racing-results/Ayr/2023-02-01/1500/Weatherbys-Cheltenham-Festival-Guide-Handicap-Chase",
        //"meetingName":"Ayr",
        //"meetingId":"3",
        //"venueName":"Ayr",
        //"ageRestriction":"5+ years",
        //"displayDistance":"2m 5½f",
        //"going":"Soft",
        //"numberOfRunners":8,
        //"raceClass":"3",
        //"raceType":"Chase",
        //"ratingBand":"0-125",
        //"surfaceType":"Turf",
        //"countDown":"Now",
        //"countryCode":"GB",
        //"bettingReturns":{
        //   "currency":"GBP",
        //   "exacta":"£30.70",
        //   "jackpot":"£0.00",
        //   "place1":"£1.80",
        //   "place2":"£2.20",
        //   "place3":"£1.90",
        //   "place4":"",
        //   "placepot":"£0.00",
        //   "quadpot":"£0.00",
        //   "rule4Text":"",
        //   "rule4Value":"",
        //   "straightForecast":"£25.19",
        //   "toteWin":"£5.00",
        //   "tricast":"£130.49",
        //   "trifecta":"£161.30"
        //},
        //"liveOn":[
        //   "RTV"
        //],
        //"replayDetails":[
        //   {
        //      "videoId":2176037,
        //      "videoProvider":"RUK",
        //      "completeRaceUid":2949846,
        //      "completeRaceStart":602,
        //      "completeRaceEnd":1039,
        //      "finishRaceUid":2949848,
        //      "finishRaceStart":905,
        //      "finishRaceEnd":970
        //   }
        //],
        //"performRaceUidATR":null,
        //"performRaceUidRUK":null,
        //"isHandicap":true,
        //"category":[
        //   "Handicap"
        //],
        //"raceTypeDescriptionText":"Chase Turf, Handicap",
        //"statusFeed":"HORSES/2023-02-01/AYR/15:00/#RACESTATUS",
        //"hybridRaceUrl":"/results/3/ayr/2023-02-01/830385",
        //"startScheduledDatetimeUTC":"2023-02-01T15:00:00+00:00"
    }
}
