using ProjectPunter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectPunter.Services
{
    public class DataService
    {

        public List<tb_country> GetAllCountries()
        {
            using (var context = new ProjectPunterEntities())
            {
                List<tb_country> result = context.Database.SqlQuery<tb_country>("pr_get_countries").ToList();

                return result;
            }
        }

        public List<tb_weather> GetWeather()
        {
            using (var context = new ProjectPunterEntities())
            {
                List<tb_weather> result = context.Database.SqlQuery<tb_weather>("pr_get_weather").ToList();

                return result;
            }
        }

        public List<tb_event> GetEvents()
        {
            using (var context = new ProjectPunterEntities())
            {
                List<tb_event> result = context.Database.SqlQuery<tb_event>("pr_get_events").ToList();

                return result;
            }
        }

        public List<tb_surface> GetSurface()
        {
            using (var context = new ProjectPunterEntities())
            {
                List<tb_surface> result = context.Database.SqlQuery<tb_surface>("pr_get_surface").ToList();

                return result;
            }
        }

        public List<tb_class> GetClass()
        {
            using (var context = new ProjectPunterEntities())
            {
                List<tb_class> result = context.Database.SqlQuery<tb_class>("pr_get_class").ToList();

                return result;
            }
        }

        public List<tb_racetype> GetRaceTypes()
        {
            using (var context = new ProjectPunterEntities())
            {
                List<tb_racetype> result = context.Database.SqlQuery<tb_racetype>("pr_get_race_types").ToList();

                return result;
            }
        }

        public List<tb_jockey> GetJockeys()
        {
            using (var context = new ProjectPunterEntities())
            {
                List<tb_jockey> result = context.Database.SqlQuery<tb_jockey>("pr_get_jockeys").ToList();

                return result;
            }
        }

        public List<tb_trainer> GetTrainers()
        {
            using (var context = new ProjectPunterEntities())
            {
                List<tb_trainer> result = context.Database.SqlQuery<tb_trainer>("pr_get_trainers").ToList();

                return result;
            }
        }

        public List<tb_horse> GetHorses()
        {
            using (var context = new ProjectPunterEntities())
            {
                List<tb_horse> result = context.Database.SqlQuery<tb_horse>("pr_get_horses").ToList();

                return result;
            }
        }

        public List<tb_racetype> GetAllRaceTypes()
        {
            using (var context = new ProjectPunterEntities())
            {
                List<tb_racetype> result = context.Database.SqlQuery<tb_racetype>("pr_get_racetypes").ToList();

                return result;
            }
        }
    }
}