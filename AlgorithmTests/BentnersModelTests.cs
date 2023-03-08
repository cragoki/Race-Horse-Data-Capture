using Core.Algorithms;
using Core.Interfaces.Algorithms;
using Core.Interfaces.Data;
using Core.Interfaces.Data.Repositories;
using Core.Models.Algorithm;
using Core.Models.Algorithm.Bentners;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using TestHelpers.GetCurrentCondition;
using TestHelpers.MappingTables;
using TestHelpers.Settings;
using Xunit;

namespace AlgorithmTests
{
    public class BentnersModelTests
    {

        #region Individual Methods
        [Fact]
        public async void ShouldGetCurrentConditionLastTwoRacesOnly()
        {

            var algorithm = InstantiateAlgorithmIndividualMethods();
            var winningHorseId = 17650;
            //Generate Race
            var race = ShouldGetCurrentConditionLastTwoRacesOnlyGenerator.ShouldGetCurrentConditionLastTwoRacesOnlyEntity();
            var settings = SettingsGenerator.GenerateAlgorithmSettings();

            var result = new List<HorsePredictionModel>();

            foreach (var horse in race.RaceHorses)
            {
                var tracker = new RaceHorseStatisticsTracker();
                var toAdd = new HorsePredictionModel();
                toAdd.horse_id = horse.horse_id;

                var currentCondition = await algorithm.GetCurrentCondition(race, horse.Horse, settings, tracker);
                toAdd.points += currentCondition.TotalPointsForGetCurrentCondition;
                result.Add(toAdd);
            }
            var maxPoints = result.Max(x => x.points);
            var winners = result.Where(x => x.points == maxPoints).ToList().Select(x => x.horse_id);

            Assert.Contains(winningHorseId, winners);
        }

        [Fact]
        public async void ShouldGetPastPerformance()
        {
            var algorithm = InstantiateAlgorithmIndividualMethods();
            var winningHorseId = 17650;
            //Generate Race
            var race = ShouldGetCurrentConditionLastTwoRacesOnlyGenerator.ShouldGetCurrentConditionLastTwoRacesOnlyEntity();
            var settings = SettingsGenerator.GenerateAlgorithmSettings();

            var result = new List<HorsePredictionModel>();

            foreach (var horse in race.RaceHorses)
            {
                var tracker = new RaceHorseStatisticsTracker();
                var toAdd = new HorsePredictionModel();
                toAdd.horse_id = horse.horse_id;

                var currentCondition = await algorithm.GetPastPerformance(race, horse.Horse, settings, tracker);
                toAdd.points += currentCondition.TotalPointsForGetCurrentCondition;
                result.Add(toAdd);
            }
            var maxPoints = result.Max(x => x.points);
            var winners = result.Where(x => x.points == maxPoints).ToList().Select(x => x.horse_id);

            Assert.Contains(winningHorseId, winners);
        }

        [Fact]
        public async void ShouldGetAdjustmentsPastPerformance()
        {

        }

        [Fact]
        public async void ShouldGetPresentRaceFactors()
        {

        }

        [Fact]
        public async void ShouldGetHorsePreferences()
        {

        }

        #endregion

        #region Overall Tests
        [Fact]
        public async void ScenarioOne()
        {

        }
        #endregion

        private IBentnersModel InstantiateAlgorithmIndividualMethods()
        {
            Mock<IConfigurationRepository> configRepoMock = new Mock<IConfigurationRepository>();
            Mock<IMappingTableRepository> mappingRepoMock = new Mock<IMappingTableRepository>();
            Mock<IAlgorithmRepository> algoRepoMock = new Mock<IAlgorithmRepository>();
            Mock<IDbContextData> context = new Mock<IDbContextData>();

            mappingRepoMock.Setup(x => x.GetDistanceTypes()).Returns(GenerateDistanceType.GenerateDistanceTypes());

            return new BentnersModel(configRepoMock.Object, mappingRepoMock.Object, algoRepoMock.Object, context.Object);
        }
    }
}