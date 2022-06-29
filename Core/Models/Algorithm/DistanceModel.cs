namespace Core.Models.Algorithm
{
    public class DistanceModel
    {
        public int Miles { get; set; }
        public int Furlongs { get; set; }
        //A furlong is exactly 201.168 metres, or 0.125 miles – so there are 8 furlongs in a mile.

        //Races in the UK and Ireland are given in miles and furlongs and you’ll see races given in furlongs for races below a mile in distance.Beyond that, distances will be in miles and furlongs.
        public int Yards { get; set; }
    }
}
