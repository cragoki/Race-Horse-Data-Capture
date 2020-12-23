using ProjectPunter.Models;
using ProjectPunter.Services;
using ProjectPunter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectPunter.Controllers
{
    public class HorseController : Controller
    {
        private HorseService _horseService;
        private ErrorService _errorService;

        public HorseController()
        {
            _horseService = new HorseService();
            _errorService = new ErrorService();
        }

        [HttpPost]
        public JsonResult AddHorse(string horseName, string dob, int countryId)
        {
            bool success = false;

            try
            {
                DateTime dateOfBirth = Convert.ToDateTime(dob);

                _horseService.AddHorse(horseName, dateOfBirth, countryId);

                success = true;
            }
            catch (Exception ex)
            {
                _errorService.LogError(ex.Message, ex.InnerException.ToString(), ex.StackTrace);
            }

            return Json(new { success}
            , JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult AddJockey(string jockeyName)
        {
            bool success = false;

            try
            {
                _horseService.AddJockey(jockeyName);
                success = true;
            }
            catch (Exception ex)
            {
                _errorService.LogError(ex.Message, ex.InnerException.ToString(), ex.StackTrace);
            }

            return Json(new { success }
            , JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddTrainer(string trainerName)
        {
            bool success = false;

            try
            {
                _horseService.AddTrainer(trainerName);
                success = true;
            }
            catch (Exception ex)
            {
                _errorService.LogError(ex.Message, ex.InnerException.ToString(), ex.StackTrace);
            }

            return Json(new { success }
            , JsonRequestBehavior.AllowGet);
        }

    }
}