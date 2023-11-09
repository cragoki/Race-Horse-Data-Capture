﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class SequenceAnalysisEntity
    {
        [Key]
        public int sequence_analysis_id { get; set; }
        public int? algorithm_id { get; set; }
        public Guid batch_id { get; set; }
        public decimal percentage_correct { get; set; }
        public int? no_of_races { get; set; }
        public decimal? percentage_correct_with_course_adjustment { get; set; }
        public int? no_of_races_with_adjustment { get; set; }
        public bool is_complete { get; set; }
        public DateTime last_checked { get; set; }
    }
}
