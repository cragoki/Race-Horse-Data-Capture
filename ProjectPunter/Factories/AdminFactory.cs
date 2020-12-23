using ProjectPunter.Models;
using ProjectPunter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectPunter.Factories
{
    public class AdminFactory
    {
        public static AdminViewModel BuildAdminModel(List<tb_country> countryList)
        {
            var result = new AdminViewModel()
            {
                CountryList = countryList
            };

            return result;
        }

    }
}