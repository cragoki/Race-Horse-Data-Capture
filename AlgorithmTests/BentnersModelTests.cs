using Core.Algorithms;
using Core.Entities;
using Core.Interfaces.Algorithms;
using Core.Interfaces.Data;
using Core.Interfaces.Data.Repositories;
using Core.Models.Algorithm;
using Moq;
using System.Collections.Generic;
using TestHelpers;
using Xunit;

namespace AlgorithmTests
{
    public class BentnersModelTests
    {

        #region Individual Methods
        [Fact]
        public async void ShouldGetCurrentConditionEasyLastTwoRacesOnly()
        {

            var algorithm = InstantiateAlgorithmIndividualMethods();
            var winningHorseId = 1;
            int numberOfHorses = 5;

            //Build Current Event
            var eventEntity = EventEntityGenerator.GetCurentEvent();
            //Get RaceHorses for Race
            var raceHorses = new List<RaceHorseEntity>();
            for (int i = 0; i >= numberOfHorses; i++)
            {
                var index = i + 1;
                var raceHorse = new RaceHorseEntity();
                if (index == winningHorseId)
                {

                }
                else 
                {
                    raceHorse = RaceHorseEntityGenerator.GenerateBasicCurrentRaceHorseEntityNoHistory(i + 1);
                }

                raceHorses.Add(raceHorse);
            }
            //Generate Race
            var race = RaceEntityGenerator.GenerateCurrentRaceEntity(eventEntity, raceHorses);


            var settings = new List<AlgorithmSettingsEntity>();
            var result = new List<HorsePredictionModel>();

            foreach (var horse in race.RaceHorses) 
            {
                var toAdd = new HorsePredictionModel();
                toAdd.points += await algorithm.GetCurrentCondition(race, horse.Horse, settings);
                result.Add(toAdd);
            }
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

            return new BentnersModel(configRepoMock.Object, mappingRepoMock.Object, algoRepoMock.Object, context.Object);
        }
    }
}