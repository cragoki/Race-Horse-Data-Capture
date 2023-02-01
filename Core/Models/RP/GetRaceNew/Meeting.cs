using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.Models.RP.GetRaceNew
{
    public class Meeting
    {
        [JsonProperty("meetingIds")]
        public List<int> EventIds { get; set; }
    }
}
