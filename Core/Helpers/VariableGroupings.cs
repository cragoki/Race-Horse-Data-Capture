using Core.Entities;
using Core.Enums;
using Core.Models.Algorithm;
using System;
using System.Collections.Generic;

namespace Core.Helpers
{
    public class VariableGroupings
    {
        public static List<DistanceGroupModel> GetDistanceGroupings(List<DistanceType> distances) 
        {
            var result = new List<DistanceGroupModel>();
            var sprints = new DistanceGroupModel() {GroupType = DistanceGroupType.Sprint, DistanceIds = new List<int>() };
            var specialist = new DistanceGroupModel() { GroupType = DistanceGroupType.Specialist, DistanceIds = new List<int>() };
            var middle = new DistanceGroupModel() { GroupType = DistanceGroupType.Middle, DistanceIds = new List<int>() };
            var staying = new DistanceGroupModel() { GroupType = DistanceGroupType.Staying, DistanceIds = new List<int>() };
            try
            {
                foreach (var distance in distances)
                {
                    var distanceParsed = DistanceBuilder(distance.distance_type);

                    //Sprints and specialist have no miles
                    if (distanceParsed.Miles == 0)
                    {
                        if (distanceParsed.Furlongs > 0)
                        {
                            if (distanceParsed.Furlongs >= 7)
                            {
                                specialist.DistanceIds.Add(distance.distance_type_id);
                            }
                            else
                            {
                                sprints.DistanceIds.Add(distance.distance_type_id);
                            }
                        }
                    }
                    else 
                    {
                        //Determine between staying and middle
                        if (distanceParsed.Miles == 1)
                        {
                            if (distanceParsed.Furlongs <= 5)
                            {
                                middle.DistanceIds.Add(distance.distance_type_id);
                            }
                            else 
                            {
                                staying.DistanceIds.Add(distance.distance_type_id);
                            }
                        }
                        else 
                        {
                            staying.DistanceIds.Add(distance.distance_type_id);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            result.Add(sprints);
            result.Add(specialist);
            result.Add(middle);
            result.Add(staying);

            return result;
        }

        public static List<FavourGroupModel> GetWeatherGroupings(List<WeatherType> weathers) 
        {
            var result = new List<FavourGroupModel>();
            var noGo = new FavourGroupModel() { FavourType = FavorGroupType.NoGo, ElementIds = new List<int>() };
            var favourable = new FavourGroupModel() { FavourType = FavorGroupType.Favourable, ElementIds = new List<int>() };
            var bad = new FavourGroupModel() { FavourType = FavorGroupType.Bad, ElementIds = new List<int>() };
            var rare = new FavourGroupModel() { FavourType = FavorGroupType.Rare, ElementIds = new List<int>() };

            try
            {
                foreach (var weather in weathers) 
                {
                    if (weather.weather_type_id == 10 || weather.weather_type_id == 18 || weather.weather_type_id == 11)
                    {
                        noGo.ElementIds.Add(weather.weather_type_id);
                    }
                    else if (weather.weather_type.ToLower().Contains("cloud") || weather.weather_type.ToLower().Contains("sun") || weather.weather_type.ToLower().Contains("light") || weather.weather_type.ToLower().Contains("breezy") || weather.weather_type.ToLower().Contains("dry"))
                    {
                        favourable.ElementIds.Add(weather.weather_type_id);
                    }
                    else if (weather.weather_type.ToLower().Contains("Rain") || weather.weather_type.ToLower().Contains("Showers"))
                    {
                        bad.ElementIds.Add(weather.weather_type_id);
                    }
                    else 
                    {
                        rare.ElementIds.Add(weather.weather_type_id);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            result.Add(noGo);
            result.Add(favourable);
            result.Add(bad);
            result.Add(rare);

            return result;
        }
        public static List<GoingGroupModel> GetGoingGroupings(List<GoingType> goings) 
        {
            var result = new List<GoingGroupModel>();
            var heavy = new GoingGroupModel() { GoingType = GoingGroupType.Heavy, ElementIds = new List<int>() };
            var firm = new GoingGroupModel() { GoingType = GoingGroupType.Firm, ElementIds = new List<int>() };
            var soft = new GoingGroupModel() { GoingType = GoingGroupType.Soft, ElementIds = new List<int>() };
            var msc = new GoingGroupModel() { GoingType = GoingGroupType.Misc, ElementIds = new List<int>() };
            try
            {
                foreach (var going in goings) 
                {
                    if (going.going_type.ToLower().Contains("heavy"))
                    {
                        heavy.ElementIds.Add(going.going_type_id);
                    }
                    else if (going.going_type.ToLower().Contains("soft") || going.going_type.ToLower().Contains("slow") || going.going_type.ToLower() == "yielding")
                    {
                        soft.ElementIds.Add(going.going_type_id);
                    }
                    else if (going.going_type.ToLower().Contains("firm") || going.going_type.ToLower().Contains("good") || going.going_type.ToLower().Contains("standard") || going.going_type.ToLower().Contains("fast"))
                    {
                        firm.ElementIds.Add(going.going_type_id);
                    }
                    else
                    {
                        msc.ElementIds.Add(going.going_type_id);
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            result.Add(heavy);
            result.Add(soft);
            result.Add(firm);
            result.Add(msc);

            return result;
        }

        public static DistanceModel DistanceBuilder(string distance)
        {
            var result = new DistanceModel();

            var distanceArray = distance.ToCharArray();
            foreach (var character in distanceArray)
            {
                if (Char.IsLetter(character))
                {

                    switch (character)
                    {
                        case 'f':
                            result.Furlongs = ExtractNumbers(distanceArray, character);
                            //
                            break;
                        case 'm':
                            result.Miles = ExtractNumbers(distanceArray, character);
                            break;
                        case 'y':
                            result.Yards = ExtractNumbers(distanceArray, character);
                            break;
                    }
                }
            }

            return result;
        }

        private static int ExtractNumbers(char[] distanceArray, char character) 
        {
            var index = Array.IndexOf(distanceArray, character);
            var numberString = "";
            for (int i = index - 1; Char.IsDigit(distanceArray[i]); i--)
            {
                if (Char.IsLetter(distanceArray[i]))
                {
                    break;
                }
                numberString += distanceArray[i].ToString();

                if (i == 0) 
                {
                    break;
                }
            }

            var arr = numberString.ToCharArray();
            Array.Reverse(arr);
            numberString = new string(arr);

            return Int32.Parse(numberString);
        }
    }
}
