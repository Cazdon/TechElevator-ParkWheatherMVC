namespace NPGeek.Web.Models
{
    /// <summary>
    /// Park class defines data for park table in database
    /// </summary>
    public class Park
    {
        public string ParkCode { get; set; }

        public string ParkName { get; set; }

        public string State { get; set; }

        public int Acreage { get; set; }

        public int ElevationInFeet { get; set; }

        public double MilesOfTrail { get; set; }

        public int NumberOfCampsites { get; set; }

        public string Climate { get; set; }

        public int YearFounded { get; set; }

        public int AnnualVisitorCount { get; set; }

        public string InspirationalQuote { get; set; }

        public string InspirationalQuoteSource { get; set; }

        public string ParkDescription { get; set; }

        public int EntryFee { get; set; }

        public int NumberOfAnimalSpecies { get; set; }
    }
}
