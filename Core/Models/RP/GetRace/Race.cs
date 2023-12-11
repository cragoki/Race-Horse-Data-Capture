using Newtonsoft.Json;

namespace Core.Models.GetRace
{
    public class Race
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("abandoned")]
        public bool Abandoned { get; set; }

        [JsonProperty("result")]
        public bool Result { get; set; }

        [JsonProperty("link")]
        public string RaceURL { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("runners")]
        public int? Runners { get; set; }

        [JsonProperty("isNextRace")]
        public bool IsNextRace { get; set; }

        [JsonProperty("hasRaceOffers")]
        public bool HasRaceOffers { get; set; }

        [JsonProperty("started")]
        public string Started { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("isThirthyMinToNextRace")]
        public bool IsThirthyMinToNextRace { get; set; }
        [JsonProperty("raceClass")]
        public int RaceClass { get; set; }
        [JsonProperty("displayDistance")]
        public string Distance { get; set; }
        [JsonProperty("ageRestriction")]
        public string Ages { get; set; }
        [JsonProperty("going")]
        public string Going { get; set; }
        [JsonProperty("weather")]
        public string Weather { get; set; }
    }
}
