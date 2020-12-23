using ProjectPunter.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjectPunter.Services
{
    public class ErrorService
    {
        public void LogError(string log, string innerException, string stack)
        {
            SqlParameter error_Log = new SqlParameter("@Horse_Name", log);
            SqlParameter error_InnerException = new SqlParameter("@Date_Of_Birth", innerException);
            SqlParameter error_Stack = new SqlParameter("@Country", stack);

                using (var context = new ProjectPunterEntities())
                {
                    context.Database.ExecuteSqlCommand("[pr_log_error] @Log, @Inner, @Stack", error_Log, error_InnerException, error_Stack);
                }
        }
    }
}