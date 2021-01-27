using ProjectPunter.Models;
using ProjectPunter.Models.Race;
using ProjectPunter.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjectPunter.Services
{
    public class RaceService
    {

        public int AddRace(int eventId, int weatherId, int surfaceId, int typeId, int classId, DateTime date, int noOfHorses)
        {
            using (var context = new ProjectPunterEntities())
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@Event_Id", eventId),
                    new SqlParameter("@Weather_Id", weatherId),
                    new SqlParameter("@Surface_Id", surfaceId),
                    new SqlParameter("@Race_Type_Id", typeId),
                    new SqlParameter("@Class_Id", classId),
                    new SqlParameter("@Number_Of_Horses", noOfHorses),
                    new SqlParameter("@Date", date)
                };
                int result = context.Database.SqlQuery<int>("pr_add_race @Event_Id, @Weather_Id, @Surface_Id, @Race_Type_Id,@Class_Id, @Number_Of_Horses, @Date", parameters).FirstOrDefault();

                return result;
            }
        }

        public tb_race GetRace(int raceId)
        {
            using (var context = new ProjectPunterEntities())
            {
                SqlParameter parameters = new SqlParameter("@Race_Id", raceId);

                tb_race result = context.Database.SqlQuery<tb_race>("pr_get_race @Race_Id", parameters).FirstOrDefault();

                return result;
            }
        }
        public List<RaceListModel> GetRaceList()
        {
            using (var context = new ProjectPunterEntities())
            {
                List<RaceListModel> result = context.Database.SqlQuery<RaceListModel>("pr_get_race_list").ToList();

                return result;
            }
        }

        public RaceModel GetRaceView(int raceId)
        {
            using (var context = new ProjectPunterEntities())
            {
                SqlParameter parameters = new SqlParameter("@race_Id", raceId);
                RaceModel result = context.Database.SqlQuery<RaceModel>("pr_get_race_view").FirstOrDefault();

                return result;
            }
        }

        public List<RaceHorseModel> GetRaceHorseListView(int raceId)
        {
            using (var context = new ProjectPunterEntities())
            {
                SqlParameter parameters = new SqlParameter("@race_Id", raceId);
                List<RaceHorseModel> result = context.Database.SqlQuery<RaceHorseModel>("pr_get_race_horse_view").ToList();

                return result;
            }
        }

        public void AddRaceHorse(tb_race_horse raceHorse)
        {
            using (var context = new ProjectPunterEntities())
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@Race_Id", raceHorse.Race_Id),
                    new SqlParameter("@Horse_Id", raceHorse.Horse_Id),
                    new SqlParameter("@Weight", raceHorse.Weight),
                    new SqlParameter("@Age", raceHorse.Age),
                    new SqlParameter("@Trainer_Id", raceHorse.Trainer_Id),
                    new SqlParameter("@Jockey_Id", raceHorse.Jockey_Id)
                };

               context.Database.ExecuteSqlCommand("pr_add_race_horse @Race_Id, @Horse_Id, @Weight, @Age, @Trainer_Id, @Jockey_Id", parameters);

            }
        }

        #region Admin Page Ajax
        public void AddWeatherCondition(string weatherCondition)
        {
            SqlParameter parameters = new SqlParameter("@Weather", weatherCondition);

            using (var context = new ProjectPunterEntities())
            {
                context.Database.ExecuteSqlCommand("pr_add_weather @Weather", parameters);
            }
        }

        public void AddSurfaceCondition(string surfaceCondition)
        {
            SqlParameter parameters = new SqlParameter("@Surface", surfaceCondition);

            using (var context = new ProjectPunterEntities())
            {
                context.Database.ExecuteSqlCommand("pr_add_surface @Surface", parameters);
            }
        }
        public void AddRaceType(string raceTypeName, bool isHurdle, int noOfHurdles, int meters, int furlongs)
        {
            SqlParameter[] parameters =
            {
                    new SqlParameter("@RaceTypeDescription", raceTypeName),
                    new SqlParameter("@IsHurdle", isHurdle),
                    new SqlParameter("@Meters", meters),
                    new SqlParameter("@Furlongs", furlongs),
                    new SqlParameter("@No_Of_Hurdles", noOfHurdles)
            };

            using (var context = new ProjectPunterEntities())
            {
                context.Database.ExecuteSqlCommand("pr_add_racetype @RaceTypeDescription, @IsHurdle, @Meters, @Furlongs, @No_Of_Hurdles", parameters);
            }
        }
        public void AddEvent(string eventName)
        {
            SqlParameter parameters = new SqlParameter("@EventName", eventName);

            using (var context = new ProjectPunterEntities())
            {
                context.Database.ExecuteSqlCommand("pr_add_event @EventName", parameters);
            }
        }
        #endregion
    }
}