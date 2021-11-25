using Core.Models.Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Variables
{
    public class Form
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public static List<int> CalculateResultsByForm(List<HorseFormModel> races, int take)
        {
            var result = new List<int>();
            var league = new Dictionary<int, int>();
            //Pass in list of race_horse where date is before date of current race being predicted
            try
            {
                var horses = races.Select(x => x.HorseId).Distinct();

                foreach (var horse in horses)
                {
                    league.Add(horse, 0);
                    foreach (var race in races.Where(x => x.HorseId == horse).OrderByDescending(x => x.Date).Take(take)) 
                    {
                        if (race.Position == 1 || race.Position == 2)
                        {
                            league[horse] += 2;
                        }
                        else if (race.Position == 3) 
                        {
                            league[horse] += 1;
                        }
                    }
                }

                //Test this adds more than 1
                result.AddRange(from entry in league orderby entry.Value descending select entry.Key);

            }
            catch (Exception ex)
            {
                Logger.Error($"!!! Error attempting to calculate results by RPR. {ex.Message}");
            }

            return result;
        }
    }
}
