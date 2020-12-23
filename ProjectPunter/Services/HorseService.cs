using ProjectPunter.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjectPunter.Services
{
    public class HorseService
    {
        private readonly ErrorService _errorService;

        public HorseService()
        {
            _errorService = new ErrorService();
        }

        public void AddHorse(string horseName, DateTime dateOfBirth, int countryId)
        {
            SqlParameter HorseName = new SqlParameter("@Horse_Name", horseName);
            SqlParameter DOB = new SqlParameter("@Date_Of_Birth", dateOfBirth);
            SqlParameter Country = new SqlParameter("@Country", countryId);

                using (var context = new ProjectPunterEntities())
                {
                    context.Database.ExecuteSqlCommand("[pr_add_horse] @Horse_Name, @Date_Of_Birth, @Country", HorseName, DOB, Country);
                }

        }

        public void AddJockey(string jockeyName)
        {
            SqlParameter JockeyName = new SqlParameter("@Jockey_Name", jockeyName);

                using (var context = new ProjectPunterEntities())
                {
                    context.Database.ExecuteSqlCommand("[pr_add_jockey] @Jockey_Name", JockeyName);
                }
        }

        public void AddTrainer(string trainerName)
        {
            SqlParameter TrainerName = new SqlParameter("@Trainer_Name", trainerName);

                using (var context = new ProjectPunterEntities())
                {
                    context.Database.ExecuteSqlCommand("[pr_add_trainer] @Trainer_Name", TrainerName);
                }
        }
    }
}