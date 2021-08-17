using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class BatchEntity
    {
        [Key]
        public Guid batch_id { get; set; }
        public string diagnostics { get; set; }
        public DateTime date { get; set; }
    }
}
