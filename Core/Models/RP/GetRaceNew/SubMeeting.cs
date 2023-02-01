using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.Models.RP.GetRaceNew
{
    public class SubMeeting
    {
        [JsonProperty("meetingId")]
        public int RacingPostId { get; set; }
        [JsonProperty("meetingStartTime")]
        public string StartTime { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("courseKey")]
        public string HashName { get; set; }
        [JsonProperty("numberOfRaces")]
        public string NumberOfRaces { get; set; }
        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }
        [JsonProperty("isMeetingAbandoned")]
        public bool IsAbandoned { get; set; }
        [JsonProperty("meetingType")]
        public string MeetingType { get; set; }
        [JsonProperty("weather")]
        public string Weather { get; set; }
        [JsonProperty("goingDetails")]
        public string SurfaceType { get; set; }
        [JsonProperty("raceIds")]
        public List<int> RaceIds { get; set; }
    }
}
