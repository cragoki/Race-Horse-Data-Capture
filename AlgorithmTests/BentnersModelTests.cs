using Core.Algorithms;
using Core.Interfaces.Algorithms;
using Core.Interfaces.Data;
using Core.Interfaces.Data.Repositories;
using Core.Models.Algorithm;
using Moq;
using System.Collections.Generic;
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
            var winningHorseId = 1;
            int numberOfHorses = 5;

            //Generate Race
            var race = ShouldGetCurrentConditionLastTwoRacesOnlyGenerator.ShouldGetCurrentConditionLastTwoRacesOnlyEntity();
            var settings = SettingsGenerator.GenerateAlgorithmSettings();

            var result = new List<HorsePredictionModel>();

            foreach (var horse in race.RaceHorses)
            {
                var toAdd = new HorsePredictionModel();
                toAdd.horse_id = horse.horse_id;
                toAdd.points += await algorithm.GetCurrentCondition(race, horse.Horse, settings);
                result.Add(toAdd);
            }

            var a = result;
        }

        [Fact]
        public async void ShouldGetCurrentConditionComplex()
        {

        }

        [Fact]
        public async void ShouldGetPastPerformanceEasy()
        {

        }

        [Fact]
        public async void ShouldGetPastPerformanceComplex()
        {

        }

        [Fact]
        public async void ShouldGetAdjustmentsPastPerformanceEasy()
        {

        }

        [Fact]
        public async void ShouldGetAdjustmentsPastPerformanceComplex()
        {

        }

        [Fact]
        public async void ShouldGetPresentRaceFactorsEasy()
        {

        }

        [Fact]
        public async void ShouldGetPresentRaceFactorsComplex()
        {

        }

        [Fact]
        public async void ShouldGetHorsePreferencesEasy()
        {

        }

        [Fact]
        public async void ShoulGetHorsePreferencesComplex()
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