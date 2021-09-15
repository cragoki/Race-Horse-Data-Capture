using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class BacklogDateEntity
    {
        [Key]
        public int backlog_id { get; set; }

        public DateTime backlog_date { get; set; }
    }
}
