using Core.Models.Settings;

namespace Core.Interfaces.Services
{
    public interface IIntervalService
    {
        Intervals GetResults();
        void IncrementNumberOfBackfills(int inc);
        void IncrementNumberOfErrorsEncountered(int inc);
        void IncrementNumberOfEvents(int inc);
        void IncrementNumberOfHorses(int inc);
        void IncrementNumberOfNewCoursesAdded(int inc);
        void IncrementNumberOfRaces(int inc);
        void ResetIntervals();
    }
}