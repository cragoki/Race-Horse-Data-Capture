using Newtonsoft.Json;

namespace Core.Models.RP.GetRaceNew
{
    public class GetRaceBase
    {
        [JsonProperty("meetings")]
        public Meeting Meetings { get; set;}
        [JsonProperty("races")]
        public Races Races { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("isLoading")]
        public bool IsLoading { get; set; }

    }
}
