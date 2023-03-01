using Core.Entities;

namespace TestHelpers.Settings
{
    public class SettingsGenerator
    {
        public static List<AlgorithmSettingsEntity> GenerateAlgorithmSettings() 
        {
            return new List<AlgorithmSettingsEntity>()
            {
                new AlgorithmSettingsEntity()
                {
                    algorithm_setting_id = 49,
                    algorithm_id = 5,
                    setting_name = "horsesrequired",
                    setting_value = "0.75"
                },
                new AlgorithmSettingsEntity()
                {
                    algorithm_setting_id = 50,
                    algorithm_id = 5,
                    setting_name = "minimumreliability",
                    setting_value = "Low"
                },
                new AlgorithmSettingsEntity()
                {
                    algorithm_setting_id = 51,
                    algorithm_id = 5,
                    setting_name = "reliabilityCurrentCondition",
                    setting_value = "2"
                },
                new AlgorithmSettingsEntity()
                {
                    algorithm_setting_id = 52,
                    algorithm_id = 5,
                    setting_name = "reliabilityPastPerformance",
                    setting_value = "2"
                },
                new AlgorithmSettingsEntity()
                {
                    algorithm_setting_id = 53,
                    algorithm_id = 5,
                    setting_name = "reliabilityAdjustmentsPastPerformance",
                    setting_value = "2"
                },
                new AlgorithmSettingsEntity()
                {
                    algorithm_setting_id = 54,
                    algorithm_id = 5,
                    setting_name = "reliabilityPresentRaceFactors",
                    setting_value = "2"
                },
                new AlgorithmSettingsEntity()
                {
                    algorithm_setting_id = 55,
                    algorithm_id = 5,
                    setting_name = "reliabilityHorsePreferences",
                    setting_value = "2"
                },
            };
        }
    }
}
