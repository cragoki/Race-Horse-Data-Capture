﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Entities
{
    public class CourseEntity
    {
        [Key]
        public int course_id { get; set; }
        public string name { get; set; }
        public string country_code { get; set; }
        public bool all_weather { get; set; }
        public string course_url { get; set; }
    }
}
