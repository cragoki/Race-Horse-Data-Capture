using AlgorithmUnitTests.Helpers;
using Core.Algorithms;
using Core.Entities;
using Core.Interfaces.Data;
using Core.Models.GetRace;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AlgorithmUnitTests
{
    public class BasicFormTests
    {
        [Fact]
        public void Test1()
        {
            var options = new DbContextOptions<DbContextData>();

            //Build Race
            var race = RaceEntityHelper.BuildSimpleRace();

            //Add 2 horses, one with a win, one with no wins
            //var raceHorses = new List<RaceHorseEntity>();
            //raceHorses.Add(RaceHorseEntityHelper.GenerateRaceHorse(1, 1, 1, 1, 0, race));
            //raceHorses.Add(RaceHorseEntityHelper.GenerateRaceHorse(2, 1, 1, 0, 0, race));


            //var contextMock = new Mock<IDbContextData>();
            //var dbSet = new Mock<DbSet<Race>>();
            //var queryable = 
            //dbSet.As<IQueryable<Race>>().Setup(m => m.Provider).Returns(queryable.Provider);
            //dbSet.As<IQueryable<Race>>().Setup(m => m.Expression).Returns(queryable.Expression);
            //dbSet.As<IQueryable<Race>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            //dbSet.As<IQueryable<Race>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            //contextMock.SetupGet(x => x.tb_race).Returns(dbSet.Object);

            //var bentnersAlgorithm = new BentnersModel(context);
        }
    }
}