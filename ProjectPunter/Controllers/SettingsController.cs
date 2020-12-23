using ProjectPunter.Factories;
using ProjectPunter.Services;
using ProjectPunter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectPunter.Controllers
{
    public class SettingsController : Controller
    {
        private DataService _dataService;
        private ErrorService _errorService;

        public SettingsController() 
        {
            _dataService = new DataService();
            _errorService = new ErrorService();
        }
        // GET: Settings

        [HttpGet]
        public ActionResult Admin()
        {
            var model = new AdminViewModel();

            try
            {
                var countryList = _dataService.GetAllCountries();

                model = AdminFactory.BuildAdminModel(countryList);


            }
            catch (Exception ex) 
            {
                _errorService.LogError(ex.Message, ex.InnerException.ToString(), ex.StackTrace);
            }

            return View(model);
        }
    }
}