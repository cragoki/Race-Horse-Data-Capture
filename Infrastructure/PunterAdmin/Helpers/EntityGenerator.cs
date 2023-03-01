using Core.Entities;
using Core.Helpers;
using Core.Models.GetRace;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Infrastructure.PunterAdmin.Helpers
{
    public class EntityGenerator
    {
        public static string GenerateEvent(string result, RaceEntity race)
        {
            //EventEntity
            result += "Event = " + TestCaseHelper.NewEntity("EventEntity");
            result += $"event_id = {race.event_id}, {Environment.NewLine}";
            result += $"course_id = {race.Event.course_id}, {Environment.NewLine}";
            result = GenerateCourse(result, race);
            result += $"abandoned = {race.Event.abandoned.ToString().ToLower()}, {Environment.NewLine}";
            if (race.Event.surface_type != null)
            {
                result += $"surface_type = {race.Event.surface_type.ToString() ?? "null"}, {Environment.NewLine}";
                result = GenerateSurfaceType(result, race);
            }
            result += $"name = {TestCaseHelper.FormatString(race.Event.name)}, {Environment.NewLine}";
            result += $"meeting_url = {TestCaseHelper.FormatString(race.Event.meeting_url)}, {Environment.NewLine}";
            result += $"hash_name = {TestCaseHelper.FormatString(race.Event.hash_name)}, {Environment.NewLine}";
            result += $"meeting_type = {race.Event.meeting_type}, {Environment.NewLine}";
            result = GenerateMeetingType(result, race);
            result += $"races = {race.Event.races}, {Environment.NewLine}";
            result += $"created = new DateTime({DateTime.Parse(race.Event.created.Date.ToString().Replace("00:00:00", ""))}), {Environment.NewLine}";
            //Event End
            result += TestCaseHelper.CloseEntity(",");
            return result;
        }
        public static string GenerateRace()
        {
            return "";
        }
        public static string GenerateRaceHorse()
        {
            return "";
        }
        public static string GenerateHorse()
        {
            return "";
        }
        public static string GenerateJockey()
        {
            return "";
        }
        public static string GenerateTrainer()
        {
            return "";
        }
        public static string GenerateWeather(string result, RaceEntity race)
        {
            //Weather Entity
            result += "Weather = " + TestCaseHelper.NewEntity("WeatherType");
            result += $"weather_type_id = {race.Weather.weather_type_id}, {Environment.NewLine}";
            result += $"weather_type = {TestCaseHelper.FormatString(race.Weather.weather_type)}, {Environment.NewLine}";
            //Weather End
            result += TestCaseHelper.CloseEntity(",");

            return result;
        }
        public static string GenerateGoing(string result, RaceEntity race)
        {
            result += "Going = " + TestCaseHelper.NewEntity("GoingType");
            result += $"going_type_id = {race.Going.going_type_id}, {Environment.NewLine}";
            result += $"going_type = {TestCaseHelper.FormatString(race.Going.going_type)}, {Environment.NewLine}";
            //Going End
            result += TestCaseHelper.CloseEntity(",");
            return result;
        }
        public static string GenerateDistance(string result, RaceEntity race)
        {
            result += "Distance = " + TestCaseHelper.NewEntity("DistanceType");
            result += $"distance_type_id = {race.Distance.distance_type_id}, {Environment.NewLine}";
            result += $"distance_type = {TestCaseHelper.FormatString(race.Distance.distance_type)}, {Environment.NewLine}";
            //Distance End
            result += TestCaseHelper.CloseEntity(",");
            return result;
        }
        public static string GenerateAges(string result, RaceEntity race)
        {
            result += "Ages = " + TestCaseHelper.NewEntity("AgeType");
            result += $"age_type_id = {race.Ages.age_type_id}, {Environment.NewLine}";
            result += $"age_type = {TestCaseHelper.FormatString(race.Ages.age_type).Replace("\n", "").Replace("\r", "").Replace(" ", "")}, {Environment.NewLine}";
            //Ages End
            result += TestCaseHelper.CloseEntity(",");
            return result;
        }
        public static string GenerateCourse(string result, RaceEntity race)
        {
            //CourseEntity
            result += "Course = " + TestCaseHelper.NewEntity("CourseEntity");
            result += $"course_id = {race.Event.course_id}, {Environment.NewLine}";
            result += $"rp_course_id = {race.Event.Course.rp_course_id ?? 0}, {Environment.NewLine}";
            result += $"name = {TestCaseHelper.FormatString(race.Event.Course.name)}, {Environment.NewLine}";
            result += $"country_code = {TestCaseHelper.FormatString(race.Event.Course.country_code)}, {Environment.NewLine}";
            result += $"all_weather = {race.Event.Course.all_weather.ToString().ToLower()}, {Environment.NewLine}";
            result += $"course_url = {TestCaseHelper.FormatString(race.Event.Course.course_url)}, {Environment.NewLine}";
            //Course End
            result += TestCaseHelper.CloseEntity(",");

            return result;
        }
        public static string GenerateSurfaceType(string result, RaceEntity race)
        {

            if (race.Event.surface_type != null)
            {
                //SurfaceType Entity
                result += "Surface = " + TestCaseHelper.NewEntity("SurfaceType");
                result += $"surface_type_id = {race.Event.surface_type.ToString() ?? "null"}, {Environment.NewLine}";
                result += $"surface_type = {TestCaseHelper.FormatString(race.Event.Surface.surface_type)}, {Environment.NewLine}";
                //SurfaceType End
                result += TestCaseHelper.CloseEntity(",");
            }
            return result;
        }
        public static string GenerateMeetingType(string result, RaceEntity race)
        {
            //MeetingType Entity
            result += "MeetingType = " + TestCaseHelper.NewEntity("MeetingType");
            result += $"meeting_type_id = {race.Event.meeting_type}, {Environment.NewLine}";
            result += $"meeting_type = {TestCaseHelper.FormatString(race.Event.MeetingType.meeting_type)}, {Environment.NewLine}";
            //MeetingType End
            result += TestCaseHelper.CloseEntity(",");

            return result;
        }
    }
}
