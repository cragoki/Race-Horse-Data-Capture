using Core.Models.Settings;
using System;

namespace Core.Interfaces.Services
{
    public interface IConfigurationService
    {
        RacingPostSettings GetRacingPostSettings();
        bool SavePermitted();
        void AddBatch(Guid batchId, string diagnostics);
    }
}