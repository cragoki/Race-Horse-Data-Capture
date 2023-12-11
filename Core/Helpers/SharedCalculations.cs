using Core.Entities;
using Core.Enums;
using Core.Models.Algorithm;
using System.Collections.Generic;
using System.Linq;

namespace Core.Helpers
{
    public class SharedCalculations
    {
        public static int GetTake(int noOfHorses)
        {
            if (noOfHorses > 1 && noOfHorses <= 4)
            {
                return 1;
            }
            else if (noOfHorses >= 5 && noOfHorses <= 7)
            {
                return 2;
            }
            else if (noOfHorses >= 8 && noOfHorses <= 15)
            {
                return 3;
            }
            else
            {
                return 4;
            }
        }

        public static ReliabilityType GetRaceReliablility(RaceEntity race, int places, DistanceGroupModel distanceGroup, GoingGroupModel goingGroup, decimal going, decimal distance)
        {
            var result = ReliabilityType.Unusable;
            var reliabilityRatings = new List<ReliabilityType>();

            foreach (var horse in race.RaceHorses)
            {
                reliabilityRatings.Add(GetHorseReliablility(horse.Horse, race, distanceGroup, goingGroup, going, distance));
            }

            var totalHorses = race.RaceHorses.Count();
            var highReliability = reliabilityRatings.Where(x => x == ReliabilityType.High).Count();
            var mediumReliability = reliabilityRatings.Where(x => x == ReliabilityType.Medium).Count();
            var lowReliability = reliabilityRatings.Where(x => x == ReliabilityType.Low).Count();

            if (reliabilityRatings.Count() == 0)
            {
                return result;
            }
            else if (highReliability > mediumReliability && (highReliability + mediumReliability) >= places)
            {
                result = ReliabilityType.High;
            }
            else if (mediumReliability > highReliability && (highReliability + mediumReliability) >= places)
            {
                result = ReliabilityType.Medium;
            }
            else if (mediumReliability > lowReliability && (lowReliability + mediumReliability) >= places)
            {
                result = ReliabilityType.Medium;
            }
            else if (lowReliability > mediumReliability && (lowReliability + mediumReliability) >= places)
            {
                result = ReliabilityType.Low;
            }
            else
            {
                result = ReliabilityType.Unusable;
            }

            return result;
        }

        public static ReliabilityType GetHorseReliablility(HorseEntity horse, RaceEntity currentRace, DistanceGroupModel distanceGroup, GoingGroupModel goingGroup, decimal going, decimal distance)
        {
            var result = ReliabilityType.Unusable;
            decimal counter = 0;

            if (horse.Races == null || horse.Races.Count() == 0 || horse.Races.All(x => x.Race.Event.created < currentRace.Event.created.AddMonths(-6)))
            {
                return ReliabilityType.Unusable;
            }

            //Get all races before the current race
            foreach (var race in horse.Races.Where(x => x.Race.Event.created < currentRace.Event.created))
            {
                if (distanceGroup.DistanceIds.Contains(race.Race.distance ?? 0))
                {
                    counter = counter + distance;
                }
                if (goingGroup.ElementIds.Contains(race.Race.going ?? 0))
                {
                    counter = counter + going;
                }
            }

            var potentialPoints = horse.Races.Count();
            var value = counter / potentialPoints * 100;

            if (value >= 75)
            {
                result = ReliabilityType.High;
            }
            else if (value >= 50)
            {
                result = ReliabilityType.Medium;
            }
            else if (value >= 25)
            {
                result = ReliabilityType.Low;
            }

            return result;
        }
    }
}
