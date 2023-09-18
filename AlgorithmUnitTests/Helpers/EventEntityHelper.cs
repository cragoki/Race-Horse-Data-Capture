using Core.Entities;
using System;

namespace AlgorithmUnitTests.Helpers
{
    public static class EventEntityHelper
    {

        public static EventEntity GenerateEvent()
        {
            return new EventEntity()
            {
                created = DateTime.Now,
                meeting_type = 1,
                MeetingType = MappingEntityHelper.GetMeetingType(),
                abandoned = false,
                Course = MappingEntityHelper.GetCourse(),
                course_id = 1,
                event_id = 1,
                Surface = MappingEntityHelper.GetSurface(),
                surface_type = 1
            };
        }

        public static EventEntity GenerateSamePastEvent(RaceEntity race)
        {
            return new EventEntity()
            {
                created = race.Event.created.AddDays(-10),
                meeting_type = race.Event.meeting_type,
                MeetingType = race.Event.MeetingType,
                abandoned = race.Event.abandoned,
                Course = race.Event.Course,
                course_id = race.Event.course_id,
                event_id = race.Event.event_id + 1,
                Surface = race.Event.Surface,
                surface_type = race.Event.surface_type
            };
        }
    }
}
