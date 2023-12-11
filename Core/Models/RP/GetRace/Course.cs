using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.Models.GetRace
{
    public class Course
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("abandoned")]
        public bool Abandoned { get; set; }

        [JsonProperty("allWeather")]
        public bool AllWeather { get; set; }

        [JsonProperty("surfaceType")]
        public string SurfaceType { get; set; }

        [JsonProperty("colour")]
        public int? Colour { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("meetingUrl")]
        public string MeetingUrl { get; set; }

        [JsonProperty("hashName")]
        public string HashName { get; set; }

        [JsonProperty("meetingTypeCode")]
        public string meetingTypeCode { get; set; }

        [JsonProperty("races")]
        public List<Race> Races { get; set; }
    }
}
