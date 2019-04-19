using System.Collections.Generic;

namespace NPGeek.Web.Models
{
    /// <summary>
    /// View model used in Park for Home / Detail page
    /// </summary>
    public class HomeDetailViewModel
    {
        public string TempScale = "F";
        public Park ParkData { get; set; }
        public List<Weather> ForecastData { get; set; } = null;
    }
}
