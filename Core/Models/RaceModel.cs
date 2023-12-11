using Core.Entities;
using System.Collections.Generic;

namespace Core.Models
{
    public class RaceModel
    {
        public string CourseUrl { get; set; }
        public string Weather { get; set; }
        public List<RaceEntity> RaceEntities { get; set; }

        //Also a list of Horse/Jocky/Trainer Entities

    }
}
