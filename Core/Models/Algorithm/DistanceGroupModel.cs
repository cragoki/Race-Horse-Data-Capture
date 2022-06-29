using Core.Enums;
using System.Collections.Generic;

namespace Core.Models.Algorithm
{
    public class DistanceGroupModel
    {
        public DistanceGroupType GroupType { get; set; }
        public List<int> DistanceIds { get; set; }
    }
}
