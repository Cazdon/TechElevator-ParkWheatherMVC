using System.Collections.Generic;
namespace NPGeek.Web.Models
{
    /// <summary>
    /// Weather class defines data elements used in weather table in database
    /// </summary>
    public class Weather
    {
        public string ParkCode { get; set; }

        public int FiveDayForecastValue { get; set; }

        public int Low { get; set; }

        public int High { get; set; }

        public string Forecast { get; set; }
        
        public List<string> Recommendations { get; set; }
    }
}
