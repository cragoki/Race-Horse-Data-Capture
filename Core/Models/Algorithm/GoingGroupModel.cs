using Core.Enums;
using System.Collections.Generic;

namespace Core.Models.Algorithm
{
    public class GoingGroupModel
    {
        public GoingGroupType GoingType { get; set; }
        public List<int> ElementIds { get; set; }
    }
}
