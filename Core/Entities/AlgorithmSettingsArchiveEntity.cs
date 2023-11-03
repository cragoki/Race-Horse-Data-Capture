using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class AlgorithmSettingsArchiveEntity
    {
        [Key]
        public int algorithm_setting_archive_id { get; set; }
        public int algorithm_setting_id { get; set; }
        public Guid batch_id { get; set; }
        public int algorithm_id { get; set; }
        [ForeignKey("algorithm_id")]
        public AlgorithmEntity Algorithm { get; set; }
        public string setting_name { get; set; }
        public string setting_value { get; set; }
    }
}
