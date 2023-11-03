using System.Collections.Generic;

namespace Core.Models.MachineLearning
{
    public class VariableAnalysisModel
    {
        public List<VariableRankings> Rankings { get; set; }
        public bool IsCorrect { get; set; }
        public int? RaceType { get; set; }
        public bool IsComplete { get; set; }
    }
}
