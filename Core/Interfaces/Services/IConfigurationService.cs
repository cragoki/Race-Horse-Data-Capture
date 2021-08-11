using Core.Models.Settings;

namespace Core.Interfaces.Services
{
    public interface IConfigurationService
    {
        RacingPostSettings GetRacingPostSettings();
    }
}