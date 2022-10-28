using Core.Entities;
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
                var formMultiplierSetting = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.formMultiplier.ToString()).FirstOrDefault().setting_value.ToString());
                var formLastXRacesSetting = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.formMultiplierLastXRaces.ToString()).FirstOrDefault().setting_value.ToString());
                var consecutivePlacementMultiplierSetting = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.consecutivePlacementMultiplier.ToString()).FirstOrDefault().setting_value.ToString());
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

            var formMultiplierSetting = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.formMultiplier.ToString()).FirstOrDefault().setting_value.ToString());
            var formLastXRacesSetting = Int32.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.formMultiplierLastXRaces.ToString()).FirstOrDefault().setting_value.ToString());
            var consecutivePlacementMultiplierSetting = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.consecutivePlacementMultiplier.ToString()).FirstOrDefault().setting_value.ToString());
            var steppingUpMultiplierSetting = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.horseSteppingUpMultiplier.ToString()).FirstOrDefault().setting_value.ToString());
            var steppingDownMultiplierSetting = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.horseSteppingDownMultiplier.ToString()).FirstOrDefault().setting_value.ToString());
            var numberOfHorsesMultiplier = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.numberOfHorsesMultiplier.ToString()).FirstOrDefault().setting_value.ToString());
            var reliablitygoing = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.reliablitygoing.ToString()).FirstOrDefault().setting_value.ToString());
            var courseMultiplier = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.courseMultiplier.ToString()).FirstOrDefault().setting_value.ToString());

            foreach (var horse in race.RaceHorses) 
            {

                //if (horse.horse_id == 26186) // Makin your mind up
                //{ 
                
                //}

                var toAdd = new FormResultModel();
                toAdd.RaceHorseId = horse.race_horse_id;
                toAdd.Points = 0;
                toAdd.PointsDescription = $"";
                toAdd.Horse = horse.Horse;
                //Get races within the last 9 months
                var races = horse.Horse.Races.Where(x => x.Race.Event.created > race.Event.created.AddMonths(-9) && x.Race.Event.created < race.Event.created).ToList();

                if (races.Count() == 0)
                {
                    toAdd.PointsDescription = "No Historic Data";
                    result.Add(toAdd);
                    continue;
                }

                var distanceOnly = races.OrderByDescending(x => x.Race.Event.created).Where(x => distanceGroup.DistanceIds.Contains(x.Race.distance ?? 0) && x.position != 0).ToList().Take(formLastXRacesSetting);

                decimal hasWonAtDistance = 0M;
                decimal classMultiplierTotal = 0M;
                decimal classDownMultiplierTotal = 0M;
                decimal numberOfHorsesTotal = 0M;
                decimal goingTotal = 0M;
                decimal courseTotal = 0M;
                decimal classAndDistanceTotal = 0M;
                decimal consecutivePlacementMultiplier = 0M;
                //Form at distance
                if (distanceOnly.Count() > 0)
                {
                    foreach (var previousRace in distanceOnly)
                    {
                        var steppingDownMultiplier = 0M;
                        var pointsForThisRace = 0M;
                        var positionToPlace = SharedCalculations.GetTake(previousRace.Race.no_of_horses ?? 0);

                        //General Victory Mutliplier 
                        if (previousRace.position == 1)
                        {
                            pointsForThisRace += (formMultiplierSetting * 2);
                            hasWonAtDistance += formMultiplierSetting * 2;
                        }
                        else if (previousRace.position <= positionToPlace)
                        {
                            var placePoints = formMultiplierSetting;
                            if (previousRace.position == 2)
                            {
                                pointsForThisRace += (placePoints);
                                hasWonAtDistance += placePoints;
                            }
                            else if (previousRace.position == 3)
                            {
                                placePoints = placePoints - 0.10M;
                                pointsForThisRace += (placePoints);
                                hasWonAtDistance += placePoints;
                            }
                            else 
                            {
                                placePoints = placePoints - 0.20M;
                                pointsForThisRace += (placePoints);
                                hasWonAtDistance += placePoints;
                            }
                        }

                        //CLASS
                        if (previousRace.Race.race_class != 0 && previousRace.Race.race_class < race.race_class)
                        {
                            if (previousRace.position <= positionToPlace)
                            {
                                pointsForThisRace += (steppingUpMultiplierSetting * 2);
                                classMultiplierTotal += steppingUpMultiplierSetting * 2;
                            }
                            else
                            {
                                pointsForThisRace += (steppingUpMultiplierSetting);
                                classMultiplierTotal += steppingUpMultiplierSetting;
                            }

                        }
                        else if (previousRace.Race.race_class != 0 && previousRace.Race.race_class > race.race_class)
                        {
                            if (previousRace.position <= positionToPlace)
                            {
                                steppingDownMultiplier = steppingDownMultiplierSetting;
                                classDownMultiplierTotal += steppingDownMultiplierSetting;
                            }
                            else
                            {
                                steppingDownMultiplier = (steppingDownMultiplierSetting * 2);
                                classDownMultiplierTotal += steppingDownMultiplierSetting * 2;
                            }
                        }



                        //NUMBER OF HORSES IN FIELD
                        if (race.no_of_horses > 8)
                        {
                            if (previousRace.Race.no_of_horses > 8) 
                            {
                                if (previousRace.position <= positionToPlace)
                                {
                                    numberOfHorsesTotal += (pointsForThisRace == 0 ? 1 : pointsForThisRace * numberOfHorsesMultiplier);
                                    pointsForThisRace += (pointsForThisRace == 0 ? 1 : pointsForThisRace * numberOfHorsesMultiplier);
                                }
                            }
                        }
                        else if(race.no_of_horses < 8)
                        {
                            if (previousRace.position <= positionToPlace)
                            {
                                numberOfHorsesTotal += (pointsForThisRace == 0 ? 1 : pointsForThisRace * numberOfHorsesMultiplier);
                                pointsForThisRace += (pointsForThisRace == 0 ? 1 : pointsForThisRace * numberOfHorsesMultiplier);
                            }
                        }

                        //GOING
                        if (goingGroup.ElementIds.Contains(previousRace.Race.going ?? 0)) 
                        {
                            if (previousRace.position == 1)
                            {
                                goingTotal += (pointsForThisRace == 0 ? 1 : pointsForThisRace * (reliablitygoing * 2));
                                pointsForThisRace += (pointsForThisRace == 0 ? 1 : pointsForThisRace * (reliablitygoing * 2));
                            }
                            else if (previousRace.position <= positionToPlace)
                            {
                                goingTotal += (pointsForThisRace == 0 ? 1 : pointsForThisRace * reliablitygoing);
                                pointsForThisRace += (pointsForThisRace == 0 ? 1 : pointsForThisRace * reliablitygoing);
                            }
                        }


                        //COURSE
                        if (previousRace.Race.Event.course_id == race.Event.course_id) 
                        {
                            if (previousRace.position == 1)
                            {
                                courseTotal += (courseMultiplier * 2);
                                pointsForThisRace += (courseMultiplier * 2);
                            }
                            if (previousRace.position <= positionToPlace)
                            {
                                courseTotal += courseMultiplier;
                                pointsForThisRace += courseMultiplier;
                            }
                        }


                        //Class and distance
                        decimal classAndDistancePoints = (formMultiplierSetting * 2);
                        if (previousRace.Race.race_class <= race.race_class && distanceGroup.DistanceIds.Contains(previousRace.Race.distance ?? 0)) 
                        {
                            if (previousRace.position == 1)
                            {
                                pointsForThisRace += (classAndDistancePoints * 2);
                                classAndDistanceTotal += classAndDistancePoints * 2;
                            }
                            else if (previousRace.position <= positionToPlace)
                            {
                                var points = classAndDistancePoints;
                                if (previousRace.position == 2)
                                {
                                    pointsForThisRace += (points);
                                    classAndDistanceTotal += points;
                                }
                                else if (previousRace.position == 3)
                                {
                                    points = points - 0.10M;
                                    pointsForThisRace += (points);
                                    classAndDistanceTotal += points;
                                }
                                else
                                {
                                    points = points - 0.20M;
                                    pointsForThisRace += (points);
                                    hasWonAtDistance += points;
                                }
                                pointsForThisRace += points;
                                classAndDistanceTotal += points;
                            }
                        }


                        //HERE we need to get which race this is in terms of order, and apply a negative multiplier depending on how long ago the race is, (potential new setting RaceDegredation multiplier)
                        //IE, the most recent race, no multiplier applied,
                        //The race before that = 1 - 0.10 (0.10 being the multiplier) and then multiply the result of that by the race points
                        //so in this case the second most recent race would be points * 0.9

                        if (steppingDownMultiplier > 0 && pointsForThisRace > 0)
                        {
                            pointsForThisRace = (pointsForThisRace * steppingDownMultiplier);
                        }

                        toAdd.Points += pointsForThisRace;
                    }
                }

                //Consecutive Placement Multiplier Setting (multiply points by setting and add result to points total)
                var lastXRaces = distanceOnly.OrderBy(x => x.Race.Event.created).Where(x => x.position != 0).Take(formLastXRacesSetting);
                var multiplierSetting = consecutivePlacementMultiplierSetting;
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
                        if (raceForm.position == 1)
                        {
                            multiplier += (multiplierSetting * 2);
                        }
                        else if (raceForm.position == 2)
                        {
                            multiplier += (multiplierSetting);
                        }
                        else if (raceForm.position == 3)
                        {
                            multiplier += (multiplierSetting - 0.10M);
                        }
                        else
                        {
                            multiplier += (multiplierSetting - 0.20M);
                        }

                        placedInLastRace = true;
                    }
                    else
                    {
                        placedInLastRace = false;
                        multiplierSetting = consecutivePlacementMultiplierSetting; //RESET THIS
                    }
                }

                consecutivePlacementMultiplier = multiplier;
                toAdd.Points += multiplier;

                if (hasWonAtDistance != 0) 
                {
                    toAdd.PointsDescription += $"Gained {hasWonAtDistance} for form at distance.\n";
                }
                if (classMultiplierTotal != 0) 
                {
                    toAdd.PointsDescription += $"Gained {classMultiplierTotal} for performances at a lower class.\n";
                }
                if (numberOfHorsesTotal != 0)
                {
                    toAdd.PointsDescription += $"Gained {numberOfHorsesTotal} for performances with a similar number of horses.\n";
                }
                if (goingTotal != 0)
                {
                    toAdd.PointsDescription += $"Gained {goingTotal} for performances at at the same going.\n";
                }
                if (courseTotal != 0)
                {
                    toAdd.PointsDescription += $"Gained {courseTotal} for performances at at the same course.\n";
                }
                if (classAndDistanceTotal != 0)
                {
                    toAdd.PointsDescription += $"Gained {classAndDistanceTotal} for performances at at the C&D.\n";
                }
                if (consecutivePlacementMultiplier != 0)
                {
                    toAdd.PointsDescription += $"Gained {consecutivePlacementMultiplier} for consecutive placements.\n";
                }
                if (classDownMultiplierTotal != 0)
                {
                    toAdd.PointsDescription += $"Class down multiplier applied to some races.";
                }

                result.Add(toAdd);
            }

            return result;
        }
    }
}
