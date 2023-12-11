using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.Models.GetRace
{
    public class DailyRaces
    {
        [JsonProperty("courses")]
        public List<Course> Courses { get; set; }
    }
}
