using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Enums
{
    public enum AlgorithmSettingEnum
    {
        //FormOnly
        horsesrequired,
        racesrequired,
        reliabilitydistance,
        reliablitygoing,
        minimumreliability,
        formmultiplierzerotoonemonths,
        formmultiplieronetotwomonths,
        formmultipliertwotothreemonths,
        formmultiplierthreetofourmonths,
        formmultiplierfourtosixmonths,
        //FormRevamp,
        //TBE - How important or detramental is a period off for the horse
        horseBreak,
        //Points for last x races
        formMultiplier,
        //The past x races to apply the form multiplier
        formMultiplierLastXRaces,
        //This value will increment, e.g -> if the value is 0.25 -> placed last race = ponts += 0.25, THEN CHECK placed last 2 races points += 0.5, THEN CHECK
        //Placed last 3 races, points += 0.75 - Totalling bonus points to 1.5
        consecutivePlacementMultiplier,
        //Both of the below -> check if horse is stepping up classes and has placed or hasnt placed, should still apply a multiplier
        horseSteppingUpMultiplier,
        horseSteppingDownMultiplier,
        numberOfHorsesMultiplier,
        courseMultiplier,
        //Bentners Model
        reliabilityCurrentCondition,
        reliabilityPastPerformance,
        reliabilityAdjustmentsPastPerformance,
        reliabilityPresentRaceFactors,
        reliabilityHorsePreferences,


        //My Model
        //Experience
        xp_track,
        xp_going,
        xp_distance,
        xp_class,
        xp_dg, //distance/going
        xp_dgc, //distance/going/class
        xp_surface, // Surface Type,
        xp_jockey,
        consistency_bonus,
        class_bonus,
        time_bonus, //time since last race
        weight_bonus,
        // Race Factors
        rf_track,
        rf_going,
        rf_distance,
        rf_class,
        rf_dg, //distance/going
        rf_dgc, //distance/going/class
        rf_surface, // Surface Type
    }
}
