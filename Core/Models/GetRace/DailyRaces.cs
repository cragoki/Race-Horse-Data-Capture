using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Core.Models.GetRace
{
    public class DailyRaces
    {
        [JsonProperty("courses")]
        public List<Course> Courses { get; set; }
    }
}
