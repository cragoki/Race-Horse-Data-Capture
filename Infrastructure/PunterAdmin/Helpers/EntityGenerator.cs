using Core.Entities;
using Core.Helpers;
using Core.Models.GetRace;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

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
            result += $"created = new DateTime({race.Event.created.ToString("yyyy,MM,dd").ToString().Replace("/", ",")}), {Environment.NewLine}";
            //Event End
            result += TestCaseHelper.CloseEntity(",");
            return result;
        }
        public static string GenerateRace(string result, RaceEntity race)
        {
            result += $"Race = {TestCaseHelper.NewEntity("RaceEntity")}";
            result += $"race_id = {race.race_id},{Environment.NewLine}";
            result += $"race_time = {TestCaseHelper.FormatString(race.race_time)},{Environment.NewLine}";
            result += $"rp_race_id = {race.rp_race_id},{Environment.NewLine}";
            result += $"weather = {race.weather},{Environment.NewLine}";
            result += $"no_of_horses = {race.no_of_horses},{Environment.NewLine}";
            result += $"going = {race.going.ToString() ?? "null"},{Environment.NewLine}";
            result += $"distance = {race.distance.ToString() ?? "null"},{Environment.NewLine}";
            result += $"race_class = {race.race_class.ToString() ?? "null"},{Environment.NewLine}";
            result += $"ages = {race.ages.ToString() ?? "null"},{Environment.NewLine}";
            result += $"completed = {race.completed.ToString().ToLower()},{Environment.NewLine}";
            result += $"event_id = {race.event_id},{Environment.NewLine}";
            //EVENT ENTITY
            result = GenerateEvent(result, race);
            //RACE END
            result += TestCaseHelper.CloseEntity(",");

            return result;
        }
        public static string GenerateRaceHorse(string result, RaceHorseEntity pastRace)
        {
            //RACE HORSE ENTITY
            result += $"{TestCaseHelper.NewEntity("RaceHorseEntity")}";
            result += $"race_horse_id = {pastRace.race_horse_id},{Environment.NewLine}";
            result += $"horse_id = {pastRace.horse_id},{Environment.NewLine}";
            result += $"weight = {TestCaseHelper.FormatString(pastRace.weight)},{Environment.NewLine}";
            result += $"age = {pastRace.age},{Environment.NewLine}";
            result += $"trainer_id = {pastRace.trainer_id},{Environment.NewLine}";
            result += $"jockey_id = {pastRace.jockey_id},{Environment.NewLine}";
            result += $"finished = {pastRace.finished.ToString().ToLower()},{Environment.NewLine}";
            result += $"position = {pastRace.position},{Environment.NewLine}";
            result += $"race_id = {pastRace.race_id},{Environment.NewLine}";
            //GENERATE RACE ENTITY, EVENT ENTITY AND POTENTIALLY RACE HORSES <- {potentially divide the above into submethods? As this work has already been done
            result = GenerateRace(result, pastRace.Race);
            //RACE HORSE END
            result += TestCaseHelper.CloseEntity(",");

            return result;
        }
        public static string GenerateHorse(string result, RaceEntity race, RaceHorseEntity horse)
        {
            //HORSE ENTITY
            result += $"Horse = {TestCaseHelper.NewEntity("HorseEntity")}";
            result += $"horse_id = {horse.horse_id},{Environment.NewLine}";
            result += $"rp_horse_id = {horse.Horse.rp_horse_id},{Environment.NewLine}";
            result += $"horse_name = {TestCaseHelper.FormatString(horse.Horse.horse_name)},{Environment.NewLine}";
            result += $"top_speed = {horse.Horse.top_speed ?? 0},{Environment.NewLine}";
            result += $"rpr = {horse.Horse.rpr ?? 0},{Environment.NewLine}";
            //GENERATE RACE HISTORY
            //Need a list of race horse entities, linked to a race with a set of race horses, then linked to an event
            result += "Races = new List<RaceHorseEntity>() {";
            result += $"{Environment.NewLine}";
            foreach (var pastRace in horse.Horse.Races.Where(x => x.Race.Event.created < race.Event.created))
            {
                result = GenerateRaceHorse(result, pastRace);
            }
            result += TestCaseHelper.CloseEntity(",");

            //GENERATE ARCHIVE
            result += "Archive = new List<HorseArchiveEntity>() {";
            result += $"{Environment.NewLine}";
            foreach (var archive in horse.Horse.Archive.Where(x => x.date <= race.Event.created))
            {
                //Archive entity
                result += $"{TestCaseHelper.NewEntity("HorseArchiveEntity")}";
                result += $"archive_id = {archive.archive_id},{Environment.NewLine}";
                result += $"horse_id = {archive.horse_id},{Environment.NewLine}";
                result += $"field_changed = {TestCaseHelper.FormatString(archive.field_changed)},{Environment.NewLine}";
                result += $"old_value = {TestCaseHelper.FormatString(archive.old_value)},{Environment.NewLine}";
                result += $"new_value = {TestCaseHelper.FormatString(archive.new_value)},{Environment.NewLine}";
                result += $"date = new DateTime({archive.date.ToString("yyyy,MM,dd").ToString().Replace("/", ",")}), {Environment.NewLine}";
                result += TestCaseHelper.CloseEntity(",");
            }
            //HORSE END
            result += TestCaseHelper.CloseEntity("");
            result += TestCaseHelper.CloseEntity(",");
            return result;
        }
        public static string GenerateJockey(string result, RaceHorseEntity horse)
        {
            result += $"Jockey = {TestCaseHelper.NewEntity("JockeyEntity")}";
            result += $"jockey_id = {horse.jockey_id},{Environment.NewLine}";
            result += $"jockey_name = {TestCaseHelper.FormatString(horse.Jockey.jockey_name)},{Environment.NewLine}";
            //Jockey END
            result += TestCaseHelper.CloseEntity(",");

            return result;
        }
        public static string GenerateTrainer(string result, RaceHorseEntity horse)
        {
            result += $"Trainer = {TestCaseHelper.NewEntity("TrainerEntity")}";
            result += $"trainer_id = {horse.trainer_id},{Environment.NewLine}";
            result += $"trainer_name = {TestCaseHelper.FormatString(horse.Trainer.trainer_name)},{Environment.NewLine}";
            //TRAINER END
            result += TestCaseHelper.CloseEntity(",");

            return result;
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
