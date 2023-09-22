using AlgorithmUnitTests.Helpers;
using Core.Algorithms;
using Core.Entities;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AlgorithmUnitTests
{
    public class BasicFormTests
    {
        [Fact]
        public async Task BasicTwoHorseRace()
        {
            //Build Race
            var race = RaceEntityHelper.BuildSimpleRace(2);

            //Add 2 horses, one with a win, one with no wins
            var raceHorses = new List<RaceHorseEntity>();
            raceHorses.Add(RaceHorseEntityHelper.GenerateRaceHorse(1, 1, 1, 0, race));
            raceHorses.Add(RaceHorseEntityHelper.GenerateRaceHorse(2, 1, 0, 0, race));
            race.RaceHorses.AddRange(raceHorses);

            var contextMock = DbSetMockHelper.GenerateContextMock();

            var bentnersAlgorithm = new BentnersModel(contextMock.Object);

            var result = await bentnersAlgorithm.RunModel(race);

            var winner = result.OrderByDescending(x => x.Points).FirstOrDefault();

            winner.horse_id.Should().Be(1);
        }

        [Fact]
        public async Task BasicCompetitiveTwoHorseRace()
        {
            //Build Race
            var race = RaceEntityHelper.BuildSimpleRace(2);

            //Add 2 horses, one with a win, one with no wins
            var raceHorses = new List<RaceHorseEntity>();
            raceHorses.Add(RaceHorseEntityHelper.GenerateRaceHorse(2, 2, 1, 1, race));
            raceHorses.Add(RaceHorseEntityHelper.GenerateRaceHorse(1, 2, 2, 0, race));
            race.RaceHorses.AddRange(raceHorses);

            var contextMock = DbSetMockHelper.GenerateContextMock();

            var bentnersAlgorithm = new BentnersModel(contextMock.Object);

            var result = await bentnersAlgorithm.RunModel(race);

            var winner = result.OrderByDescending(x => x.Points).FirstOrDefault();

            winner.horse_id.Should().Be(2);
        }

        [Fact]
        public async Task BasicPlaceOnlyHorseRace()
        {
            //Build Race
            var race = RaceEntityHelper.BuildSimpleRace(5);

            //Add 2 horses, one with a win, one with no wins
            var raceHorses = new List<RaceHorseEntity>();
            raceHorses.Add(RaceHorseEntityHelper.GenerateRaceHorse(1, 2, 0, 1, race));
            raceHorses.Add(RaceHorseEntityHelper.GenerateRaceHorse(2, 2, 0, 0, race));
            raceHorses.Add(RaceHorseEntityHelper.GenerateRaceHorse(3, 2, 0, 2, race));
            raceHorses.Add(RaceHorseEntityHelper.GenerateRaceHorse(4, 0, 0, 0, race));
            raceHorses.Add(RaceHorseEntityHelper.GenerateRaceHorse(5, 0, 0, 0, race));

            race.RaceHorses.AddRange(raceHorses);

            var contextMock = DbSetMockHelper.GenerateContextMock();

            var bentnersAlgorithm = new BentnersModel(contextMock.Object);

            var result = await bentnersAlgorithm.RunModel(race);

            var winner = result.OrderByDescending(x => x.Points).FirstOrDefault();

            winner.horse_id.Should().Be(3);
        }

        [Fact]
        public async Task WinTakesPriorityOverPlace()
        {
            //Build Race
            var race = RaceEntityHelper.BuildSimpleRace(5);

            //Add 2 horses, one with a win, one with no wins
            var raceHorses = new List<RaceHorseEntity>();
            raceHorses.Add(RaceHorseEntityHelper.GenerateRaceHorse(1, 3, 2, 1, race));
            raceHorses.Add(RaceHorseEntityHelper.GenerateRaceHorse(2, 3, 2, 0, race));
            raceHorses.Add(RaceHorseEntityHelper.GenerateRaceHorse(3, 3, 1, 2, race));
            raceHorses.Add(RaceHorseEntityHelper.GenerateRaceHorse(4, 3, 0, 3, race));
            raceHorses.Add(RaceHorseEntityHelper.GenerateRaceHorse(5, 3, 3, 0, race));

            race.RaceHorses.AddRange(raceHorses);

            var contextMock = DbSetMockHelper.GenerateContextMock();

            var bentnersAlgorithm = new BentnersModel(contextMock.Object);

            var result = await bentnersAlgorithm.RunModel(race);

            var winner = result.OrderByDescending(x => x.Points).FirstOrDefault();

            winner.horse_id.Should().Be(5);
        }
    }
}