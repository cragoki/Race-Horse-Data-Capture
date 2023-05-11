using Core.Entities;

namespace Core.Models.Algorithm
{
    public class HorsePredictionModel
    {
        public int horse_id { get; set; }
        public int race_horse_id { get; set; }
        public decimal points { get; set; }
        public AlgorithmTrackerEntity tracker { get; set; }
    }
}
