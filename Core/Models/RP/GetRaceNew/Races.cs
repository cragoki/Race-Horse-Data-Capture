using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.Models.RP.GetRaceNew
{
    public class Races
    {
        [JsonProperty("raceIds")]
        public List<int> RaceIds { get; set; }
    }
}
