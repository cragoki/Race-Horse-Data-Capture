using System.ComponentModel.DataAnnotations;


namespace Core.Entities
{
    public class JockeyEntity
    {
        [Key]
        public int jockey_id { get; set; }
        public string jockey_name { get; set; }
        public string jockey_url { get; set; }
    }
}
