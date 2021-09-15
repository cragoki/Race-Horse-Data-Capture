
using Core.Interfaces.Services;
using Core.Models.Settings;

namespace Core.Services
{
    public class IntervalService : IIntervalService
    {
        public int NumberOfEvents;
        public int NumberOfRaces;
        public int NumberOfNewHorsesAdded;
        public int NumberOfNewCoursesAdded;
        public int NumberOfErrorsEncountered;
        public int NumberOfBackfills;

        public IntervalService()
        {

        }

        public void ResetIntervals()
        {
            NumberOfEvents = 0;
            NumberOfRaces = 0;
            NumberOfNewHorsesAdded = 0;
            NumberOfNewCoursesAdded = 0;
            NumberOfErrorsEncountered = 0;
            NumberOfBackfills = 0;
        }

        public void IncrementNumberOfEvents(int inc)
        {
            NumberOfEvents = NumberOfEvents + inc;
        }

        public void IncrementNumberOfRaces(int inc)
        {
            NumberOfRaces = NumberOfRaces + inc;
        }

        public void IncrementNumberOfHorses(int inc)
        {
            NumberOfNewHorsesAdded = NumberOfNewHorsesAdded + inc;
        }
        public void IncrementNumberOfNewCoursesAdded(int inc)
        {
            NumberOfNewCoursesAdded = NumberOfNewCoursesAdded + inc;
        }

        public void IncrementNumberOfErrorsEncountered(int inc)
        {
            NumberOfErrorsEncountered = NumberOfErrorsEncountered + inc;
        }

        public void IncrementNumberOfBackfills(int inc)
        {
            NumberOfBackfills = NumberOfBackfills + inc;
        }

        public Intervals GetResults()
        {
            return new Intervals()
            {
                NumberOfRaces = NumberOfRaces,
                NumberOfEvents = NumberOfEvents,
                NumberOfBackfills = NumberOfBackfills,
                NumberOfErrorsEncountered = NumberOfErrorsEncountered,
                NumberOfNewCoursesAdded = NumberOfNewCoursesAdded,
                NumberOfNewHorsesAdded = NumberOfNewHorsesAdded
            };
        }
    }
}
