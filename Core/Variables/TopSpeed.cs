﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Variables
{
    public class TopSpeed
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public static List<HorseEntity> CalculateResultsByTopSpeed(List<HorseEntity> race)
        {
            var result = new List<HorseEntity>();
            try
            {
                foreach (var horse in race)
                {
                    if (horse.top_speed == null || horse.top_speed == 0)
                    {
                        horse.top_speed = 0;
                    }
                }

                result = race.OrderByDescending(i => i.top_speed).ToList();

            }
            catch (Exception ex)
            {
                Logger.Error($"!!! Error attempting to calculate results by top speed. {ex.Message}");
            }

            return result;
        }

    }
}
