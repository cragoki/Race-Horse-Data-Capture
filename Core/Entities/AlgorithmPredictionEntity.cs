using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities
{
    public class AlgorithmPredictionEntity
    {
        [Key]
        public int algorithm_prediction_id { get; set; }

        public int race_horse_id { get; set; }
        [ForeignKey("race_horse_id")]
        public RaceHorseEntity RaceHorse { get; set; }
        public int algorithm_id { get; set; }
        [ForeignKey("algorithm_id")]
        public AlgorithmEntity Algorithm { get; set; }
        public int predicted_position { get; set; }
        public decimal points { get; set; }
        public string points_description { get; set; }

    }
}
