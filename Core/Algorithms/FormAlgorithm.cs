using Core.Entities;
using Core.Enums;
using Core.Helpers;
using Core.Interfaces.Algorithms;
using Core.Interfaces.Data.Repositories;
using Core.Interfaces.Services;
using Core.Models.Algorithm;
using Core.Variables;
using Infrastructure.PunterAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Algorithms
{
    public class FormAlgorithm : IFormAlgorithm
    {
        private readonly IConfigurationRepository _configRepository;
        private readonly IMappingTableRepository _mappingRepository;

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public FormAlgorithm(IConfigurationRepository configRepository, IMappingTableRepository mappingRepository)
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

        public async Task<decimal> CalculateHorsePredictability(List<AlgorithmSettingsEntity> settings, HorseEntity horse, int? currentRaceId, List<DistanceGroupModel> distanceGroups, List<GoingGroupModel> goingGroups, decimal distance) 
        {
            var result = 0;
            var raceCounter = 0;
            var correctPredictions = 0;
            try
            {
                foreach (var races in horse.Races) 
                {
                    var race = races.Race;
                    var total = SharedCalculations.GetTake(race.no_of_horses ?? 0);
                    var horseWillPlace = false;

                    if (race.race_id == currentRaceId) 
                    {
                        continue;
                    }

                    var distanceGroup = distanceGroups.Where(x => x.DistanceIds.Contains(race.distance ?? 0)).FirstOrDefault();
                    var goingGroup = goingGroups.Where(x => x.ElementIds.Contains(race.going ?? 0)).FirstOrDefault();

                    var predictions = await AssignHorsePoints(settings, race, distanceGroup, goingGroup, distance);

                    //Get Actual results and compare vs horsePoints
                    var placedHorses = race.RaceHorses.Where(x => x.position != 0 && x.position <= total).Select(x => x.horse_id).ToList();
                    var predictedPlacedhorses = predictions.OrderByDescending(x => x.Points).Take(total).Select(x => x.Horse.horse_id).ToList();

                    if (predictions.All(x => x.Points == 0)) 
                    {
                        continue;
                    }

                    if (predictedPlacedhorses.Any(x => x == horse.horse_id)) 
                    {
                        horseWillPlace = true;
                    }


                    if (horseWillPlace && predictedPlacedhorses.Any(x => x == horse.horse_id)) 
                    {
                        correctPredictions++;
                    }
                    else 
                    {
                        if (!horseWillPlace && !predictedPlacedhorses.Any(x => x == horse.horse_id))
                        {
                            correctPredictions++;
                        }
                    }

                    raceCounter++;
                }

                if (raceCounter > 0)
                {
                    result = correctPredictions / raceCounter * 100;
                }
                else 
                {
                    result = -1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }

        private async Task<List<FormResultModel>> AssignHorsePoints(List<AlgorithmSettingsEntity> settings, RaceEntity race, DistanceGroupModel distanceGroup,GoingGroupModel goingGroup, decimal distance) 
        {
            var result = new List<FormResultModel>();

            foreach (var horse in race.RaceHorses)
            {
                var toAdd = new FormResultModel();
                toAdd.RaceHorseId = horse.race_horse_id;
                toAdd.Points = 0;
                toAdd.Horse = horse.Horse;
                //Get races within the last 6 months
                var races = horse.Horse.Races.Where(x => x.Race.Event.created > race.Event.created.AddMonths(-6) && x.Race.Event.created < race.Event.created).ToList();

                if (races.Count() == 0)
                {
                    result.Add(toAdd);
                    continue;
                }

                var allConditions = races.Where(x => distanceGroup.DistanceIds.Contains(x.Race.distance ?? 0) && goingGroup.ElementIds.Contains(x.Race.going ?? 0)).ToList();
                var distanceOnly = races.Where(x => distanceGroup.DistanceIds.Contains(x.Race.distance ?? 0) && !allConditions.Any(y => y.race_id == x.race_id)).ToList();

                //Loop through horses who have met the identical conditions within the last 6 months
                if (allConditions.Count() > 0)
                {
                    foreach (var idealRace in allConditions)
                    {
                        var multiplier = GetFormMultiplier(settings, idealRace, race.Event.created);
                        decimal points = 3M;

                        var placed = SharedCalculations.GetTake(idealRace.Race.no_of_horses ?? 0);
                        if (idealRace.position == 1)
                        {
                            toAdd.Points = toAdd.Points + (points * multiplier);
                        }
                        else if (idealRace.position <= placed && idealRace.position != 0)
                        {
                            points = 1.5M;
                            toAdd.Points = toAdd.Points + (points * multiplier);
                        }
                    }
                }

                //Loop through horses who have met the identical Distance within the last 6 months, then multiply the points assigned by the distance variance
                if (distanceOnly.Count() > 0)
                {
                    foreach (var distanceOnlyRace in distanceOnly)
                    {
                        var multiplier = GetFormMultiplier(settings, distanceOnlyRace, race.Event.created);
                        decimal points = 3M;

                        var placed = SharedCalculations.GetTake(distanceOnlyRace.Race.no_of_horses ?? 0);
                        if (distanceOnlyRace.position == 1)
                        {
                            toAdd.Points = toAdd.Points + ((points * distance) * multiplier);
                        }
                        else if (distanceOnlyRace.position <= placed && distanceOnlyRace.position != 0)
                        {
                            points = 1.5M;
                            toAdd.Points = toAdd.Points + ((points * distance) * multiplier);
                        }
                    }
                }
                result.Add(toAdd);
            }

            return result;
        }

        private decimal GetFormMultiplier(List<AlgorithmSettingsEntity> settings, RaceHorseEntity race, DateTime currentRaceDate)
        {
            var result = 0M;
            var raceDate = race.Race.Event.created;
            var monthDiff = ((currentRaceDate.Year - raceDate.Year) * 12) + currentRaceDate.Month - raceDate.Month;

            switch (monthDiff)
            {
                case 0:
                    result = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.formmultiplierzerotoonemonths.ToString()).FirstOrDefault().setting_value.ToString());
                    break;
                case int n when (n == 1 || n == 2):
                    result = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.formmultiplieronetotwomonths.ToString()).FirstOrDefault().setting_value.ToString());
                    break;
                case int n when (n == 3):
                    result = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.formmultipliertwotothreemonths.ToString()).FirstOrDefault().setting_value.ToString());
                    break;
                case int n when (n == 4):
                    result = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.formmultiplierthreetofourmonths.ToString()).FirstOrDefault().setting_value.ToString());
                    break;
                case int n when (n == 5 || n == 6):
                    result = Decimal.Parse(settings.Where(x => x.setting_name == AlgorithmSettingEnum.formmultiplierfourtosixmonths.ToString()).FirstOrDefault().setting_value.ToString());
                    break;
            }

            return result;
        }
    }

}
