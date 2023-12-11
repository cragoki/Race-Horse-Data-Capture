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
    public class StandardDifferentials
    {

        [Fact]
        public async Task HorseWithNewJockeyVsHorseWithJockeyExperience()
        {
            //Build Race
            var race = RaceEntityHelper.BuildSimpleRace(5);

            //Add 4 horses with the exact same history, other than the fact that horses 1 and 4's jockeys have better average position
            var raceHorses = new List<RaceHorseEntity>();
            raceHorses.Add(RaceHorseEntityHelper.GenerateRaceHorse(1, 2, 1, 0, race));
            raceHorses.Add(RaceHorseEntityHelper.GenerateRaceHorse(2, 2, 1, 0, race));
            raceHorses.Add(RaceHorseEntityHelper.GenerateRaceHorse(3, 2, 1, 0, race));
            raceHorses.Add(RaceHorseEntityHelper.GenerateRaceHorse(4, 2, 1, 0, race));

            race.RaceHorses.AddRange(raceHorses);

            var contextMock = DbSetMockHelper.GenerateContextMock();

            //Build Jockey Data
            var jockeyData = new List<RaceHorseEntity>();
            jockeyData.Add(RaceHorseEntityHelper.GenerateJockeyHistory(2, 1)); //Horse 2's jockey with a good average
            jockeyData.Add(RaceHorseEntityHelper.GenerateJockeyHistory(2, 3));
            jockeyData.Add(RaceHorseEntityHelper.GenerateJockeyHistory(2, 2));
            jockeyData.Add(RaceHorseEntityHelper.GenerateJockeyHistory(2, 1));

            jockeyData.Add(RaceHorseEntityHelper.GenerateJockeyHistory(4, 2)); //Horse 4's jockey with a good average
            jockeyData.Add(RaceHorseEntityHelper.GenerateJockeyHistory(4, 4));
            jockeyData.Add(RaceHorseEntityHelper.GenerateJockeyHistory(4, 2));
            jockeyData.Add(RaceHorseEntityHelper.GenerateJockeyHistory(4, 3));

            jockeyData.Add(RaceHorseEntityHelper.GenerateJockeyHistory(3, 9)); //Horse 3's jockey with a worse average
            jockeyData.Add(RaceHorseEntityHelper.GenerateJockeyHistory(3, 6));
            jockeyData.Add(RaceHorseEntityHelper.GenerateJockeyHistory(3, 3));
            jockeyData.Add(RaceHorseEntityHelper.GenerateJockeyHistory(3, 12));

            jockeyData.Add(RaceHorseEntityHelper.GenerateJockeyHistory(1, 9)); //Horse 1's jockey with a worse average
            jockeyData.Add(RaceHorseEntityHelper.GenerateJockeyHistory(1, 8));
            jockeyData.Add(RaceHorseEntityHelper.GenerateJockeyHistory(1, 9));
            jockeyData.Add(RaceHorseEntityHelper.GenerateJockeyHistory(1, 6));

            //Add in jockey Data
            contextMock.Setup(c => c.tb_race_horse).Returns(DbSetMockHelper.GetJockeyHistory(jockeyData).Object);

            var bentnersAlgorithm = new BentnersModel(contextMock.Object);

            var result = await bentnersAlgorithm.RunModel(race);

            var winner = result.OrderByDescending(x => x.Points).FirstOrDefault();

            winner.horse_id.Should().BeOneOf(4, 2);
        }

        [Fact]
        public async Task FormShouldTakePriorityOverJockeyHistory()
        {
            //Build Race
            var race = RaceEntityHelper.BuildSimpleRace(5);

            //Add 4 horses with the exact same history, other than the fact that horses 1 and 4's jockeys have better average position
            var raceHorses = new List<RaceHorseEntity>();
            raceHorses.Add(RaceHorseEntityHelper.GenerateRaceHorse(1, 3, 1, 2, race));
            raceHorses.Add(RaceHorseEntityHelper.GenerateRaceHorse(2, 3, 0, 2, race));
            raceHorses.Add(RaceHorseEntityHelper.GenerateRaceHorse(3, 3, 3, 0, race));
            raceHorses.Add(RaceHorseEntityHelper.GenerateRaceHorse(4, 3, 2, 1, race));

            race.RaceHorses.AddRange(raceHorses);

            var contextMock = DbSetMockHelper.GenerateContextMock();

            //Build Jockey Data
            var jockeyData = new List<RaceHorseEntity>();
            jockeyData.Add(RaceHorseEntityHelper.GenerateJockeyHistory(2, 1)); //Horse 2's jockey with a good average
            jockeyData.Add(RaceHorseEntityHelper.GenerateJockeyHistory(2, 3));
            jockeyData.Add(RaceHorseEntityHelper.GenerateJockeyHistory(2, 2));
            jockeyData.Add(RaceHorseEntityHelper.GenerateJockeyHistory(2, 1));

            jockeyData.Add(RaceHorseEntityHelper.GenerateJockeyHistory(4, 2)); //Horse 4's jockey with a good average
            jockeyData.Add(RaceHorseEntityHelper.GenerateJockeyHistory(4, 4));
            jockeyData.Add(RaceHorseEntityHelper.GenerateJockeyHistory(4, 2));
            jockeyData.Add(RaceHorseEntityHelper.GenerateJockeyHistory(4, 3));

            jockeyData.Add(RaceHorseEntityHelper.GenerateJockeyHistory(3, 9)); //Horse 3's jockey with a worse average
            jockeyData.Add(RaceHorseEntityHelper.GenerateJockeyHistory(3, 6));
            jockeyData.Add(RaceHorseEntityHelper.GenerateJockeyHistory(3, 3));
            jockeyData.Add(RaceHorseEntityHelper.GenerateJockeyHistory(3, 12));

            jockeyData.Add(RaceHorseEntityHelper.GenerateJockeyHistory(1, 9)); //Horse 1's jockey with a worse average
            jockeyData.Add(RaceHorseEntityHelper.GenerateJockeyHistory(1, 8));
            jockeyData.Add(RaceHorseEntityHelper.GenerateJockeyHistory(1, 9));
            jockeyData.Add(RaceHorseEntityHelper.GenerateJockeyHistory(1, 6));

            //Add in jockey Data
            contextMock.Setup(c => c.tb_race_horse).Returns(DbSetMockHelper.GetJockeyHistory(jockeyData).Object);

            var bentnersAlgorithm = new BentnersModel(contextMock.Object);

            var result = await bentnersAlgorithm.RunModel(race);

            var winner = result.OrderByDescending(x => x.Points).FirstOrDefault();

            winner.horse_id.Should().Be(3);
        }

        [Fact]
        public async Task HorseWithBetterHistoryAtCourseShouldWin()
        {
            //Build Race
            var race = RaceEntityHelper.BuildSimpleRace(5);

            //Add 4 horses with the exact same history, other than the fact that horses 1 and 4's jockeys have better average position
            var raceHorses = new List<RaceHorseEntity>();
            raceHorses.Add(RaceHorseEntityHelper.GenerateRaceHorse(1, 3, 3, 0, race));
            raceHorses.Add(RaceHorseEntityHelper.GenerateRaceHorse(2, 3, 3, 0, race));
            raceHorses.Add(RaceHorseEntityHelper.GenerateRaceHorse(3, 3, 2, 1, race));
            raceHorses.Add(RaceHorseEntityHelper.GenerateRaceHorse(4, 3, 1, 2, race));

            race.RaceHorses.AddRange(raceHorses);

            var contextMock = DbSetMockHelper.GenerateContextMock();

            foreach (var r in race.RaceHorses.Where(x => x.horse_id == 1).FirstOrDefault().Horse.Races)
            {
                r.Race.Event.course_id = race.Event.course_id + 1;
            }

            var bentnersAlgorithm = new BentnersModel(contextMock.Object);

            var result = await bentnersAlgorithm.RunModel(race);

            var winner = result.OrderByDescending(x => x.Points).FirstOrDefault();

            winner.horse_id.Should().Be(2);
        }

        [Fact]
        public async Task HorseWithBetterHistoryAtGoingShouldWin()
        {
            //Build Race
            var race = RaceEntityHelper.BuildSimpleRace(5);

            //Add 4 horses with the exact same history, other than the fact that horses 1 and 4's jockeys have better average position
            var raceHorses = new List<RaceHorseEntity>();
            raceHorses.Add(RaceHorseEntityHelper.GenerateRaceHorse(1, 3, 3, 0, race));
            raceHorses.Add(RaceHorseEntityHelper.GenerateRaceHorse(2, 3, 3, 0, race));
            raceHorses.Add(RaceHorseEntityHelper.GenerateRaceHorse(3, 3, 2, 1, race));
            raceHorses.Add(RaceHorseEntityHelper.GenerateRaceHorse(4, 3, 1, 2, race));

            race.RaceHorses.AddRange(raceHorses);

            var contextMock = DbSetMockHelper.GenerateContextMock();

            foreach (var r in race.RaceHorses.Where(x => x.horse_id == 1).FirstOrDefault().Horse.Races)
            {
                r.Race.going = 2;
                r.Race.Going = new GoingType()
                {
                    going_type_id = 2,
                    going_type = "Soft"
                };
            }

            var bentnersAlgorithm = new BentnersModel(contextMock.Object);

            var result = await bentnersAlgorithm.RunModel(race);

            var winner = result.OrderByDescending(x => x.Points).FirstOrDefault();

            winner.horse_id.Should().Be(2);
        }

        [Fact]
        public async Task HorseWithBetterHistoryAtLowerClassShouldWin()
        {
            //Build Race
            var race = RaceEntityHelper.BuildSimpleRace(5);

            //Add 4 horses with the exact same history, other than the fact that horses 1 and 4's jockeys have better average position
            var raceHorses = new List<RaceHorseEntity>();
            raceHorses.Add(RaceHorseEntityHelper.GenerateRaceHorse(1, 3, 3, 0, race));
            raceHorses.Add(RaceHorseEntityHelper.GenerateRaceHorse(2, 3, 3, 0, race));
            raceHorses.Add(RaceHorseEntityHelper.GenerateRaceHorse(3, 3, 2, 1, race));
            raceHorses.Add(RaceHorseEntityHelper.GenerateRaceHorse(4, 3, 1, 2, race));

            race.RaceHorses.AddRange(raceHorses);

            var contextMock = DbSetMockHelper.GenerateContextMock();

            foreach (var r in race.RaceHorses.Where(x => x.horse_id == 2).FirstOrDefault().Horse.Races)
            {
                r.Race.race_class = 4; //Default is 5
            }

            var bentnersAlgorithm = new BentnersModel(contextMock.Object);

            var result = await bentnersAlgorithm.RunModel(race);

            var winner = result.OrderByDescending(x => x.Points).FirstOrDefault();

            winner.horse_id.Should().Be(2);
        }

        [Fact]
        public async Task HorseWithBetterHistoryAtEvenLowerClassShouldWin()
        {
            //Build Race
            var race = RaceEntityHelper.BuildSimpleRace(5);

            //Add 4 horses with the exact same history, other than the fact that horses 1 and 4's jockeys have better average position
            var raceHorses = new List<RaceHorseEntity>();
            raceHorses.Add(RaceHorseEntityHelper.GenerateRaceHorse(1, 3, 3, 0, race));
            raceHorses.Add(RaceHorseEntityHelper.GenerateRaceHorse(2, 3, 3, 0, race));
            raceHorses.Add(RaceHorseEntityHelper.GenerateRaceHorse(3, 3, 3, 0, race));
            raceHorses.Add(RaceHorseEntityHelper.GenerateRaceHorse(4, 3, 1, 2, race));

            race.RaceHorses.AddRange(raceHorses);

            var contextMock = DbSetMockHelper.GenerateContextMock();

            foreach (var r in race.RaceHorses.Where(x => x.horse_id == 2).FirstOrDefault().Horse.Races)
            {
                r.Race.race_class = 4; //Default is 5
            }

            foreach (var r in race.RaceHorses.Where(x => x.horse_id == 3).FirstOrDefault().Horse.Races)
            {
                r.Race.race_class = 3; //Default is 5
            }

            var bentnersAlgorithm = new BentnersModel(contextMock.Object);

            var result = await bentnersAlgorithm.RunModel(race);

            var winner = result.OrderByDescending(x => x.Points).FirstOrDefault();

            winner.horse_id.Should().Be(3);
        }
    }
}