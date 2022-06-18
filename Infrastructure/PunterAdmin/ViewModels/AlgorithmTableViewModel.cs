using System.Collections.Generic;

namespace Infrastructure.PunterAdmin.ViewModels
{
    public class AlgorithmTableViewModel
    {
        public int AlgorithmId { get; set; }
        public string AlgorithmName { get; set; }
        public bool IsActive { get; set; }
        public decimal? Accuracy { get; set; }
        public int? NumberOfRaces { get; set; }
        public string Notes { get; set; }
        public bool ShowSettings { get; set; }
        public bool ShowVariables { get; set; }
        public List<AlgorithSettingsTableViewModel> Settings { get; set; }
        public List<AlgorithmVariableTableViewModel> Variables { get; set; }

    }
}
