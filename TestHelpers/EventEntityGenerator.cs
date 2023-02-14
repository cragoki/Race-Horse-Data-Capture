using Core.Entities;
using Core.Models.GetRace;

namespace TestHelpers
{
    public class EventEntityGenerator
    {

        public static EventEntity GetCurentEvent() 
        {
            return new EventEntity()
            {
                event_id = 1,
                course_id = 1,
                Course = new CourseEntity()
                {
                    course_id = 1,
                    rp_course_id = 100,
                    name = "Wacky Races",
                    country_code = "GB",
                    all_weather = false,
                    course_url = ""
                },
                abandoned = false,
                surface_type = 1,
                Surface = new SurfaceType()
                {
                    surface_type_id = 1,
                    surface_type = "Polytrack"
                },
                name = "Wacky Races Unit Tests",
                meeting_url = "",
                hash_name = "wackyraces",
                meeting_type = 1,
                MeetingType = new MeetingType()
                {
                    meeting_type_id = 1,
                    meeting_type = "Flat",
                },
                races = 1,
                batch_id = Guid.NewGuid(),
                created = DateTime.Now
            };
        }

        public static EventEntity GetPastEvent(int daysBefore)
        {
            return new EventEntity()
            {
                event_id = 1,
                course_id = 1,
                Course = new CourseEntity()
                {
                    course_id = 1,
                    rp_course_id = 100,
                    name = "Wacky Races",
                    country_code = "GB",
                    all_weather = false,
                    course_url = ""
                },
                abandoned = false,
                surface_type = 1,
                Surface = new SurfaceType()
                {
                    surface_type_id = 1,
                    surface_type = "Polytrack"
                },
                name = "Wacky Races Unit Tests",
                meeting_url = "",
                hash_name = "wackyraces",
                meeting_type = 1,
                MeetingType = new MeetingType()
                {
                    meeting_type_id = 1,
                    meeting_type = "Flat",
                },
                races = 1,
                batch_id = Guid.NewGuid(),
                created = DateTime.Now.AddDays(daysBefore)
            };
        }
    }
}
