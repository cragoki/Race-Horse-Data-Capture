using ProjectPunter.Factories;
using ProjectPunter.Models;
using ProjectPunter.Services;
using ProjectPunter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;

namespace ProjectPunter.Controllers
{
    public class RaceController : Controller
    {
        private DataService _dataService;
        private RaceService _raceService;
        private ErrorService _errorService;

        public RaceController()
        {
            _dataService = new DataService();
            _raceService = new RaceService();
            _errorService = new ErrorService();
        }

        // GET: Race
        [HttpGet]
        public ActionResult AddRace()
        {
            AddRaceViewModel model = new AddRaceViewModel();
            model.EventList = _dataService.GetEvents();
            model.ClassList = _dataService.GetClass();
            model.RaceType = _dataService.GetRaceTypes();
            model.WeatherList = _dataService.GetWeather();
            model.SurfaceType = _dataService.GetSurface();
            return View(model);
        }

        [HttpPost]
        public JsonResult AddRace(int eventId, int weatherId, int surfaceId, int typeId, int classId, DateTime date, int noOfHorses)
        {
            var newRaceId =_raceService.AddRace(eventId, weatherId, surfaceId, typeId, classId, date, noOfHorses);
            return Json(new { success = true, redirectUrl = "AddRaceHorse?raceId=" + newRaceId.ToString()}, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddRaceHorse(int raceId)
        {
            AddRaceHorseViewModel model = new AddRaceHorseViewModel();
            model.RaceId = raceId;
            model.HorseList = _dataService.GetHorses();
            model.JockeyList = _dataService.GetJockeys();
            model.TrainerList = _dataService.GetTrainers();
            model.CountryList = _dataService.GetAllCountries();
            tb_race newRace = _raceService.GetRace(raceId);
            model.NoOfHorses = newRace.Number_Of_Horses;
            return View(model);
        }

        [HttpPost]
        public ActionResult AddRaceHorse(AddRaceHorseViewModel raceHorse)
        {
            try
            {
                //Retrieve List of Horses for extra Info
                var horses = _dataService.GetHorses();

                //Build Model
                var result = RaceFactory.BuildRaceHorseModel(raceHorse, horses);

                //Add to DB
                foreach (var horse in result)
                {
                    _raceService.AddRaceHorse(horse);
                }
            }
            catch (Exception ex)
            {
                _errorService.LogError(ex.Message, ex.InnerException.ToString(), ex.StackTrace);
            }

            //Redirect to AddRace() to start again
            return RedirectToAction("AddRace","Race");
        }

        [HttpGet]
        public ActionResult ViewRaces() 
        {
            var model = new RaceListViewModel();

            try
            {
                model.RaceList = _raceService.GetRaceList();
            }
            catch (Exception ex) 
            {
                _errorService.LogError(ex.Message, ex.InnerException.ToString(), ex.StackTrace);
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult ViewRace(int raceId) 
        {
            //Get Race Details from ID (pr_get_race)
            var raceInfo = _raceService.GetRaceView(raceId);

            //Get the list of horses which are involved in the race (pr_get_race_horse)
            var raceHorses = _raceService.GetRaceHorseListView(raceId);

            //Create a factory method to build a view model which will ultimately populate the view
            var model = RaceFactory.BuildRaceModel(raceHorses, raceInfo);

            return View(model);
        }



        #region AJAX Endpoints

        [HttpPost]
        public JsonResult AddRaceType(string raceTypeName, bool isHurdle, int noOfHurdles, int meters, int furlongs) 
        {
            var result = false;
            try
            {
                _raceService.AddRaceType(raceTypeName, isHurdle, noOfHurdles, meters, furlongs);
                result = true;

            }
            catch (Exception ex)
            {
                _errorService.LogError(ex.Message, ex.InnerException.ToString(), ex.StackTrace);
            }

            return Json(new { success = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddWeatherCondition(string weatherCondition)
        {
            var result = false;
            try
            {
                _raceService.AddWeatherCondition(weatherCondition);
                 result = true;
            }
            catch (Exception ex)
            {
                _errorService.LogError(ex.Message, ex.InnerException.ToString(), ex.StackTrace);
            }

            return Json(new { success = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddEvent(string eventName)
        {
            var result = false;
            try
            {
                _raceService.AddEvent(eventName);
                result = true;

            }
            catch (Exception ex)
            {
                _errorService.LogError(ex.Message, ex.InnerException.ToString(), ex.StackTrace);
            }

            return Json(new { success = result }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddSurfaceCondition(string surfaceCondition)
        {
            var result = false;
            try
            {
                _raceService.AddSurfaceCondition(surfaceCondition);
                result = true;
            }
            catch (Exception ex)
            {
                _errorService.LogError(ex.Message, ex.InnerException.ToString(), ex.StackTrace);
            }

            return Json(new { success = result }, JsonRequestBehavior.AllowGet);
        }

        

        #endregion
    }
}