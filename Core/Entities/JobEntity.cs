﻿using Core.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class JobEntity
    {
        [Key]
        public int job_id { get; set; }
        public string job_name { get; set; }
        public DateTime? last_execution { get; set; }
        public DateTime next_execution { get; set; }
        public int interval_check_minutes { get; set; }
        public bool? start { get; set; }
        public AlgorithmModeEnum? mode { get; set; }

    }
}
