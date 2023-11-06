using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class SequenceCourseAccuracyEntity
    {
        [Key]
        public int sequence_course_accuracy_id { get; set; }
        public Guid batch_id { get; set; }
        public int course_id { get; set; }
        [ForeignKey("course_id")]
        public CourseEntity Course { get; set; }
        public int number_of_races { get; set; }
        public decimal percentage_correct { get; set; }

    }
}
