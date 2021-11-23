using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Variables
{
    public class RPR
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public static List<HorseEntity> CalculateResultsByRPR(List<HorseEntity> race)
        {
            var result = new List<HorseEntity>();

            try
            {
                foreach (var horse in race)
                {
                    if (horse.rpr == null || horse.rpr == 0)
                    {
                        horse.rpr = 0;
                    }
                }

                result = race.OrderByDescending(i => i.rpr).ToList();

            }
            catch (Exception ex)
            {
                Logger.Error($"!!! Error attempting to calculate results by RPR. {ex.Message}");
            }

            return result;
        }
    }
}
