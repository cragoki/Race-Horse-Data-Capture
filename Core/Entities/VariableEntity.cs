using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class VariableEntity
    {
        [Key]
        public int variable_id { get; set; }
        public string variable_name { get; set; }
    }
}
