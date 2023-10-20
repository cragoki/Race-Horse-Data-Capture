using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class AlgorithmTrackerEntity
    {
        [Key]
        public int algorithm_tracker_id { get; set; }
        public int algorithm_id { get; set; }
        [ForeignKey("algorithm_id")]
        public AlgorithmEntity Algorithm { get; set; }
        public int race_horse_id { get; set; }
        [ForeignKey("race_horse_id")]
        public RaceHorseEntity RaceHorse { get; set; }
        public decimal total_points { get; set; }

        #region Bentners Model
        public decimal total_points_for_get_current_condition { get; set; }
        public decimal total_points_for_last_two_races { get; set; }
        public decimal total_points_for_time_since_last_race { get; set; }
        public string get_current_condition_description { get; set; }
        public decimal total_points_for_past_performance { get; set; }
        public string get_past_performance_description { get; set; }
        public decimal total_points_for_adjustments_past_performance { get; set; }
        public decimal total_points_for_strength_of_competition { get; set; }
        public decimal total_points_for_weight { get; set; }
        public decimal total_points_for_jockey_contribution { get; set; }
        public string get_past_performance_adjustments_description { get; set; }
        public decimal points_given_for_jockey { get; set; }
        public decimal points_given_for_trainer { get; set; }
        public string get_present_race_factors_description { get; set; }
        public decimal total_points_for_specific_track { get; set; }
        public decimal total_points_for_distance { get; set; }
        public decimal total_points_for_race_type { get; set; }
        public decimal total_points_for_going { get; set; }
        public string get_horse_preferences_description { get; set; }

        #endregion
        #region My Model
        public decimal? points_xp_track { get; set; }
        public decimal? points_xp_going { get; set; }
        public decimal? points_xp_distance { get; set; }
        public decimal? points_xp_class { get; set; }
        public decimal? points_xp_dg { get; set; }
        public decimal? points_xp_dgc { get; set; }
        public decimal? points_xp_surface { get; set; }
        public decimal? points_xp_jockey { get; set; }
        public decimal? points_consistency_bonus { get; set; }
        public decimal? points_class_bonus { get; set; }
        public decimal? points_time_bonus { get; set; }
        public decimal? points_weight_bonus { get; set; }
        public decimal? points_rf_track { get; set; }
        public decimal? points_rf_going { get; set; }
        public decimal? points_rf_distance { get; set; }
        public decimal? points_rf_class { get; set; }
        public decimal? points_rf_dg { get; set; }
        public decimal? points_rf_dgc { get; set; }
        public decimal? points_rf_surface { get; set; }
        #endregion
        public DateTime created { get; set; }
    }
}
