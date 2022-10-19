﻿using Core.Entities;
using Core.Enums;
using Core.Helpers;
using Core.Interfaces.Algorithms;
using Core.Interfaces.Data.Repositories;
using Core.Interfaces.Services;
using Core.Models.Algorithm;
using Infrastructure.PunterAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Algorithms
{
    public class FormRevamped : IFormRevamped
    {
        private readonly IConfigurationRepository _configRepository;
        private readonly IMappingTableRepository _mappingRepository;

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public FormRevamped(IConfigurationRepository configRepository, IMappingTableRepository mappingRepository)
        {
            _configRepository = configRepository;
            _mappingRepository = mappingRepository;
        }

        public async Task<AlgorithmResult> GenerateAlgorithmResult(List<RaceEntity> races, List<AlgorithmVariableEntity> algorithms)
        {
            var result = new AlgorithmResult();
            try
            {
                List<double> runningTotal = new List<double>();
                int raceCounter = 0;

                foreach (var race in races)
                {
                    var predictionResult = await FormCalculation(race, algorithms);

                    if (predictionResult != -1)
                    {
                        Console.WriteLine($"Valid Prediction with {predictionResult}%");
                        raceCounter++;
                        runningTotal.Add(predictionResult);
                    }
                }

                //get average of running total and store result
                if (raceCounter > 0)
                {
                    result.Accuracy = (decimal)runningTotal.Average();
                    result.RacesFiltered = raceCounter;
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Error in Algorithm Service...{ex.Message}");
            }

            return result;
        }

        public async Task<double> FormCalculation(RaceEntity race, List<AlgorithmVariableEntity> variables)
        {
            var settings = _configRepository.GetAlgorithmSettings((int)AlgorithmEnum.FormOnly);
            var total = SharedCalculations.GetTake(race.no_of_horses ?? 0);
            var counter = 0;
            var horses = race.RaceHorses;
            var listOfHorses = new List<HorseEntity>();
            var going = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.reliablitygoing.ToString()).FirstOrDefault().setting_value.ToString());
            var distance = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.reliabilitydistance.ToString()).FirstOrDefault().setting_value.ToString());

            var minimumReliability = settings.Where(x => x.setting_name == AlgorithmSettingEnum.minimumreliability.ToString())?.FirstOrDefault().setting_value.ToString();
            var distances = _mappingRepository.GetDistanceTypes();
            var goings = _mappingRepository.GetGoingTypes();

            var distanceGroups = VariableGroupings.GetDistanceGroupings(distances).Where(x => x.DistanceIds.Contains(race.distance ?? 0)).FirstOrDefault();
            var goingGroups = VariableGroupings.GetGoingGroupings(goings).Where(x => x.ElementIds.Contains(race.going ?? 0)).FirstOrDefault();

            var reliability = SharedCalculations.GetRaceReliablility(race, total, distanceGroups, goingGroups, going, distance);

            if (race.RaceHorses.All(x => x.position == 0))
            {
                return -1;
            }

            //Reliability checks
            if (Enum.TryParse(minimumReliability, out ReliabilityType minimum))
            {
                if (minimum == ReliabilityType.High)
                {
                    if (reliability != ReliabilityType.High)
                    {
                        return -1;
                    }
                }
                else if (minimum == ReliabilityType.Medium)
                {
                    if (reliability != ReliabilityType.High && reliability != ReliabilityType.Medium)
                    {
                        return -1;
                    }
                }
                else
                {
                    if (reliability == ReliabilityType.Unusable)
                    {
                        return -1;
                    }
                }
            }
            else
            {
                return -1;
            }

            //BEGIN CALCULATING PREDICTED RESULTS
            var horsepoints = new List<HorsePredictionModel>();

            foreach (var horse in race.RaceHorses)
            {
                var toAdd = new HorsePredictionModel();
                toAdd.points = 0;
                toAdd.horse_id = horse.horse_id;
                //Get races within the last 6 months
                var races = horse.Horse.Races.Where(x => x.Race.Event.created > race.Event.created.AddMonths(-6) && x.Race.Event.created < race.Event.created).ToList();

                if (races.Count() == 0)
                {
                    horsepoints.Add(toAdd);
                    continue;
                }
                var allConditions = new List<RaceHorseEntity>();
                if (race.race_class != null && race.race_class != 0)
                {
                    allConditions = races.Where(x => distanceGroups.DistanceIds.Contains(x.Race.distance ?? 0) && goingGroups.ElementIds.Contains(x.Race.going ?? 0) && x.Race.race_class <= race.race_class).ToList();
                }
                else
                {
                    allConditions = races.Where(x => distanceGroups.DistanceIds.Contains(x.Race.distance ?? 0) && goingGroups.ElementIds.Contains(x.Race.going ?? 0)).ToList();
                }
                var distanceOnly = races.Where(x => distanceGroups.DistanceIds.Contains(x.Race.distance ?? 0) && !allConditions.Any(y => y.race_id == x.race_id)).ToList();


                //Loop through horses who have met the identical conditions within the last 6 months
                if (allConditions.Count() > 0)
                {
                    foreach (var idealRace in allConditions)
                    {
                        var placed = SharedCalculations.GetTake(idealRace.Race.no_of_horses ?? 0);
                        //TODO: Implement class multiplier here, points * multiplier for each class lower (I.E 0.20 for 1 class lower, 0.40 for 2 classes lower, -0.20 for one class higher)
                        if (idealRace.position == 1)
                        {
                            toAdd.points = toAdd.points + 3;
                        }
                        else if (idealRace.position <= placed && idealRace.position != 0)
                        {
                            toAdd.points = toAdd.points + 1;
                        }
                    }
                }

                //Loop through horses who have met the identical Distance within the last 6 months, then multiply the points assigned by the distance variance
                if (distanceOnly.Count() > 0)
                {
                    foreach (var distanceOnlyRace in distanceOnly)
                    {
                        var placed = SharedCalculations.GetTake(distanceOnlyRace.Race.no_of_horses ?? 0);
                        if (distanceOnlyRace.position == 1)
                        {
                            toAdd.points = toAdd.points + (3 * distance);
                        }
                        else if (distanceOnlyRace.position <= placed && distanceOnlyRace.position != 0)
                        {
                            toAdd.points = toAdd.points + (1 * distance);
                        }
                    }
                }

                horsepoints.Add(toAdd);
            }

            //Get Actual results and compare vs horsePoints
            var placedHorses = race.RaceHorses.Where(x => x.position != 0 && x.position <= total);
            var predictedPlacedhorses = horsepoints.OrderByDescending(x => x.points).Take(total);

            foreach (var placedHorse in placedHorses)
            {
                if (predictedPlacedhorses.Any(x => x.horse_id == placedHorse.horse_id))
                {
                    counter++;
                }
            }

            return (double)counter / total * 100;
        }

        public async Task<List<FormResultModel>> FormCalculationPredictions(RaceEntity race, List<AlgorithmSettingsEntity> settings, List<DistanceType> distances, List<GoingType> goings)
        {
            var result = new List<FormResultModel>();

            try
            {
                var total = SharedCalculations.GetTake(race.no_of_horses ?? 0);
                var horses = race.RaceHorses;
                var listOfHorses = new List<HorseEntity>();

                //NEW SETTINGS
                var horseBreakSetting = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.horseBreak.ToString()).FirstOrDefault().setting_value.ToString());
                var formMultiplierSetting = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.formMultiplier.ToString()).FirstOrDefault().setting_value.ToString());
                var formLastXRacesSetting = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.formMultiplierLastXRaces.ToString()).FirstOrDefault().setting_value.ToString());
                var consecutivePlacementMultiplierSetting = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.consecutivePlacementMultiplier.ToString()).FirstOrDefault().setting_value.ToString());
                var steppingUpMultiplierPlacedSetting = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.horseSteppingUpMultiplierPlaced.ToString()).FirstOrDefault().setting_value.ToString());
                var steppingUpMultiplierNotPlacedSetting = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.horseSteppingUpMultiplierNotPlaced.ToString()).FirstOrDefault().setting_value.ToString());
                var classLastXRacesSetting = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.horseSteppingUpMultiplierLastXRaces.ToString()).FirstOrDefault().setting_value.ToString());
                var steppingDownMultiplierSetting = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.horseSteppingDownMultiplier.ToString()).FirstOrDefault().setting_value.ToString());


                var going = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.reliablitygoing.ToString()).FirstOrDefault().setting_value.ToString());
                var distance = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.reliabilitydistance.ToString()).FirstOrDefault().setting_value.ToString());
                var minimumReliability = settings.Where(x => x.setting_name == AlgorithmSettingEnum.minimumreliability.ToString())?.FirstOrDefault().setting_value.ToString();

                var distanceGroups = VariableGroupings.GetDistanceGroupings(distances).ToList();
                var goingGroups = VariableGroupings.GetGoingGroupings(goings).ToList();

                var distanceGroup = distanceGroups.Where(x => x.DistanceIds.Contains(race.distance ?? 0)).FirstOrDefault();
                var goingGroup = goingGroups.Where(x => x.ElementIds.Contains(race.going ?? 0)).FirstOrDefault();

                var reliability = SharedCalculations.GetRaceReliablility(race, total, distanceGroup, goingGroup, going, distance);

                //Reliability checks
                if (Enum.TryParse(minimumReliability, out ReliabilityType minimum))
                {
                    if (minimum == ReliabilityType.High)
                    {
                        if (reliability != ReliabilityType.High)
                        {
                            return result;
                        }
                    }
                    else if (minimum == ReliabilityType.Medium)
                    {
                        if (reliability != ReliabilityType.High && reliability != ReliabilityType.Medium)
                        {
                            return result;
                        }
                    }
                    else
                    {
                        if (reliability == ReliabilityType.Unusable)
                        {
                            return result;
                        }
                    }
                }
                else
                {
                    return result;
                }

                //Calculate results 
                result = await AssignHorsePoints(settings, race, distanceGroup, goingGroup, distance);

                foreach (var horse in result)
                {
                    var horsePredictability = 0;// await CalculateHorsePredictability(settings, horse.Horse, race.race_id, distanceGroups, goingGroups, distance);
                    horse.Predictability = horsePredictability;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }

        private async Task<List<FormResultModel>> AssignHorsePoints(List<AlgorithmSettingsEntity> settings, RaceEntity race, DistanceGroupModel distanceGroup, GoingGroupModel goingGroup, decimal distance)
        {
            var result = new List<FormResultModel>();

            var horseBreakSetting = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.horseBreak.ToString()).FirstOrDefault().setting_value.ToString());
            var formMultiplierSetting = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.formMultiplier.ToString()).FirstOrDefault().setting_value.ToString());
            var formLastXRacesSetting = Int32.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.formMultiplierLastXRaces.ToString()).FirstOrDefault().setting_value.ToString());
            var consecutivePlacementMultiplierSetting = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.consecutivePlacementMultiplier.ToString()).FirstOrDefault().setting_value.ToString());



            foreach (var horse in race.RaceHorses)
            {
                var toAdd = new FormResultModel();
                toAdd.RaceHorseId = horse.race_horse_id;
                toAdd.Points = 0;
                toAdd.PointsDescription = $"";
                toAdd.Horse = horse.Horse;
                //Get races within the last 6 months
                var races = horse.Horse.Races.Where(x => x.Race.Event.created > race.Event.created.AddMonths(-6) && x.Race.Event.created < race.Event.created).ToList();

                if (races.Count() == 0)
                {
                    result.Add(toAdd);
                    continue;
                }

                var allConditions = races.OrderByDescending(x => x.Race.Event.created).Where(x => distanceGroup.DistanceIds.Contains(x.Race.distance ?? 0) && goingGroup.ElementIds.Contains(x.Race.going ?? 0)).ToList().Take(formLastXRacesSetting);
                var distanceOnly = races.OrderByDescending(x => x.Race.Event.created).Where(x => distanceGroup.DistanceIds.Contains(x.Race.distance ?? 0) && !allConditions.Any(y => y.race_id == x.race_id)).ToList().Take(formLastXRacesSetting - allConditions.Count());
                var allRaces = new List<RaceHorseEntity>();
                allRaces.AddRange(allConditions.ToList());
                allRaces.AddRange(distanceOnly.ToList());

                //Loop through horses who have met the identical conditions within the last 6 months
                if (allConditions.Count() > 0)
                {
                    foreach (var idealRace in allConditions)
                    {
                        decimal points = formMultiplierSetting;

                        var placed = SharedCalculations.GetTake(idealRace.Race.no_of_horses ?? 0);
                        if (idealRace.position == 1)
                        {
                            toAdd.Points = toAdd.Points + (points * 2);
                            toAdd.PointsDescription += $"Won at all Conditions +{points * 2} <br>";
                        }
                        else if (idealRace.position <= placed && idealRace.position != 0)
                        {
                            //determine new points
                            points = (points * 0.75M);
                            toAdd.Points = toAdd.Points + (points);
                            toAdd.PointsDescription += $"Placed at all Conditions +{points} <br>";

                        }
                    }
                }

                //Loop through horses who have met the identical Distance within the last 6 months, then multiply the points assigned by the distance variance
                if (distanceOnly.Count() > 0)
                {
                    foreach (var distanceOnlyRace in distanceOnly)
                    {
                        decimal points = (formMultiplierSetting * distance);

                        var placed = SharedCalculations.GetTake(distanceOnlyRace.Race.no_of_horses ?? 0);
                        if (distanceOnlyRace.position == 1)
                        {
                            toAdd.Points = toAdd.Points + (points * 2);
                            toAdd.PointsDescription += $"Won at Distance Only +{points * 2} <br>";

                        }
                        else if (distanceOnlyRace.position <= placed && distanceOnlyRace.position != 0)
                        {
                            //determine new points
                            toAdd.Points = toAdd.Points + (points);
                            toAdd.PointsDescription += $"Placed at Distance Only +{points} <br>";
                        }
                    }
                }

                //Consecutive Placement Multiplier Setting (multiply points by setting and add result to points total)
                var lastXRaces = allRaces.OrderByDescending(x => x.Race.Event.created).Where(x => x.position != 0).Take(formLastXRacesSetting);
                var multiplierSetting = formMultiplierSetting;
                decimal multiplier = 0;
                var placedInLastRace = false;
                //NEED TO TEST THIS OUT, SEE IF IT WORKS
                foreach (var raceForm in lastXRaces)
                {
                    var placed = SharedCalculations.GetTake(raceForm.Race.no_of_horses ?? 0);

                    if (raceForm.position <= placed && raceForm.position != 0)
                    {
                        if (placedInLastRace)
                        {
                            multiplierSetting = (multiplierSetting + formMultiplierSetting);
                        }
                        multiplier += multiplierSetting;
                        placedInLastRace = true;
                    }
                    else
                    {
                        placedInLastRace = false;
                    }
                }

                toAdd.Points = toAdd.Points + multiplier;
                toAdd.PointsDescription += $"Gained {multiplier} Points for Consecutive Placement Multiplier <br>";

                //ALSO NEED TO TEST THIS
                //Determine class step up/down to ammend the points
                toAdd = CalculateClassAdjustmentMultiplier(settings, races, race, toAdd);
                //HERE
                result.Add(toAdd);
            }

            return result;
        }

        private FormResultModel CalculateClassAdjustmentMultiplier(List<AlgorithmSettingsEntity> settings, List<RaceHorseEntity> races, RaceEntity race, FormResultModel points) 
        {
            var result = points;
            var multiplier = 0M;
            bool steppingUp = false;
            bool steppingDown = false;
            var steppingUpMultiplierPlacedSetting = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.horseSteppingUpMultiplierPlaced.ToString()).FirstOrDefault().setting_value.ToString());
            var steppingUpMultiplierNotPlacedSetting = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.horseSteppingUpMultiplierNotPlaced.ToString()).FirstOrDefault().setting_value.ToString());
            var classLastXRacesSetting = Int32.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.horseSteppingUpMultiplierLastXRaces.ToString()).FirstOrDefault().setting_value.ToString());
            var steppingDownMultiplierSetting = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.horseSteppingDownMultiplier.ToString()).FirstOrDefault().setting_value.ToString());

            var racesToUse = races.OrderByDescending(x => x.Race.Event.created).Take(classLastXRacesSetting);




           //Stepping down
           if (racesToUse.Where(x => x.position != 0).Any(x => x.Race.race_class > race.race_class) && racesToUse.Where(x => x.position != 0).All(x => x.Race.race_class != race.race_class)) 
           {
               steppingDown = true;
           }
           //Stepping Up
           if (racesToUse.Where(x => x.position != 0).Any(x => x.Race.race_class < race.race_class) && racesToUse.Where(x => x.position != 0).All(x => x.Race.race_class != race.race_class))
           {
               steppingUp = true;
               var positionToPlace = SharedCalculations.GetTake(race.no_of_horses ?? 0);


               if (racesToUse.Any(x => x.position <= positionToPlace))
               {
                    //Get difference between classes
                    var classDiff = race.race_class - (racesToUse.OrderBy(x => x.Race.race_class).FirstOrDefault().Race.race_class);
                    var pointsToAdd = steppingUpMultiplierPlacedSetting * classDiff;
                    multiplier += pointsToAdd ?? 0;
               }
               else 
               {
                    //Get difference between classes
                    var classDiff = race.race_class - (racesToUse.OrderBy(x => x.Race.race_class).FirstOrDefault().Race.race_class);
                    var pointsToAdd = steppingUpMultiplierNotPlacedSetting * classDiff;
                    multiplier += pointsToAdd ?? 0;
               }
           }
            

            //Stepping down
            if (steppingDown && !steppingUp)
            {
                result.PointsDescription += $"Multiplying {result.Points} by stepping down multiplier {steppingDownMultiplierSetting} as horse is stepping down classes. <br>";
                result.Points = (result.Points * steppingDownMultiplierSetting) ?? 0;
            }
            //Stepping up
            else if (!steppingDown && steppingUp)
            {
                points.PointsDescription += $"Adding {multiplier} to point total as horse is stepping up classes. <br>";
                result.Points += (result.Points + multiplier) ?? 0;
            }
            else 
            {
                result.PointsDescription += $"No Class multiplier applied <br>";
            }

            return result;
        }
    }
}
