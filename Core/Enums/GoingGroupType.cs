namespace Core.Enums
{
    public enum GoingGroupType
    {
        //Firm ground is often found in the summer during the Flat season when the racing surface is very dry. A dry surface means horses can run faster and often results in the quickest race times.
        Firm,
        //On the slower side of firm, but still a quick surface. Often if the ground is firm, racecourse staff will add water to the track, especially if there is no rain forecast.
        GoodToFirm,
        //The most common type of ground and arguably the fairest for the majority of horses.
        ////It is easy to run on and tracks will often try to ensure good ground in order to suit a wide range of horses and attract bigger fields.
        Good,
        //Often occurring in the winter months, good to soft ground is mostly good ground but which is also holding a fair bit of water.
        GoodToSoft,
        //Soft ground is common in the jumps season as the weather tends to be much wetter and the temperature is much lower.
        ////This surface is much harder for horses to run on and, as the ground is deeper and moister, horses run much slower. Some horses prefer this going and will run exclusively on ground that is soft.
        Soft,
        //A real test of a racehorse’s stamina and only very few horses relish this type of ground.
        //It is often very wet and hard to run on as the water soaks into the ground. Often described as a ‘bog’, with reference to how slow this surface rides.
        Heavy,
        //Nogo
        Abandoned,
        //Fallback for those that defy the logic
        Misc

    }
}
