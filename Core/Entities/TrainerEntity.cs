using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class TrainerEntity
    {
        [Key]
        public int trainer_id { get; set; }
        public string trainer_name { get; set; }
        public string trainer_url { get; set; }

    }
}
