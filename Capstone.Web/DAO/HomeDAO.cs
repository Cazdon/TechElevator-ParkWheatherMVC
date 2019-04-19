using NPGeek.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace NPGeek.Web.DAO
{
    public class HomeDAO : IHomeDAO
    {
        #region Member Variables
        /// <summary>
        /// local storage for database connection parameters
        /// </summary>
        private readonly string _connectionString;
        #endregion

        #region Constructor
        /// <summary>
        /// Home Controller Data Access class
        /// </summary>
        /// <param name="connectionstring">Database conection parameters</param>
        public HomeDAO(string connectionstring)
        {
            _connectionString = connectionstring;
        }
        #endregion

        #region public Methods
        /// <summary>
        /// Retrieve Park information for Home/Index view
        /// </summary>
        /// <returns>IList<HomeViewModel> List of park information to display</returns>
        public IList<HomeViewModel> GetAllParks()
        {

            IList<HomeViewModel> parks = new List<HomeViewModel>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM park", conn);

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var park = new HomeViewModel()
                        {
                            ParkCode = Convert.ToString(reader["parkCode"]),
                            ParkName = Convert.ToString(reader["parkName"]),
                            State = Convert.ToString(reader["state"]),
                            ParkDescription = Convert.ToString(reader["parkDescription"])
                        };

                        parks.Add(park);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return parks;
        }

        /// <summary>
        /// Get full park information and weather information for Detail page
        /// </summary>
        /// <param name="pCode">Park code identifies park for display</param>
        /// <returns>HomeDetailViewModel Full Park / weather information</returns>
        public HomeDetailViewModel GetDetailViewModel(string pCode)
        {
            HomeDetailViewModel result = new HomeDetailViewModel();

            Park park = new Park();
            park = GetParkDetail(pCode);

            List<Weather> forecast = new List<Weather>();
            forecast = GetForecastData(pCode);

            result.ParkData = park;
            result.ForecastData = forecast;

            return result;
        }
        #endregion

        #region private Methods
        /// <summary>
        /// PRIVATE Get full park information on selected park
        /// </summary>
        /// <param name="pCode">park code of park information to retreive</param>
        /// <returns></returns>
        private Park GetParkDetail(string pCode)
        {
            Park park = new Park();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM park where parkCode = @pCode", conn);
                    cmd.Parameters.AddWithValue("@pCode", pCode);

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        park = new Park()
                        {
                            ParkCode = Convert.ToString(reader["parkCode"]),
                            ParkName = Convert.ToString(reader["parkName"]),
                            State = Convert.ToString(reader["state"]),
                            Acreage = Convert.ToInt32(reader["acreage"]),
                            ElevationInFeet = Convert.ToInt32(reader["elevationInFeet"]),
                            MilesOfTrail = Convert.ToDouble(reader["milesOfTrail"]),
                            NumberOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]),
                            Climate = Convert.ToString(reader["climate"]),
                            YearFounded = Convert.ToInt32(reader["yearFounded"]),
                            AnnualVisitorCount = Convert.ToInt32(reader["annualVisitorCount"]),
                            InspirationalQuote = Convert.ToString(reader["inspirationalQuote"]),
                            InspirationalQuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]),
                            ParkDescription = Convert.ToString(reader["parkDescription"]),
                            EntryFee = Convert.ToInt32(reader["entryFee"]),
                            NumberOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"])
                        };
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return park;
        }

        /// <summary>
        /// PRIVATE Get full weather information on selected park
        /// </summary>
        /// <param name="pCode">park code of weather information to retreive</param>
        /// <returns>List<Weather> List contains 5-day weather forecast information</returns>
        private List<Weather> GetForecastData(string pCode)
        {
            List<Weather> weathers = new List<Weather>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM weather WHERE parkCode = @pCode ORDER BY fiveDayForecastValue", conn);
                    cmd.Parameters.AddWithValue("@pCode", pCode);


                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var weather = new Weather()
                        {
                            ParkCode = Convert.ToString(reader["parkCode"]),
                            FiveDayForecastValue = Convert.ToInt32(reader["fiveDayForecastValue"]),
                            Low = Convert.ToInt32(reader["low"]),
                            High = Convert.ToInt32(reader["high"]),
                            Forecast = Convert.ToString(reader["forecast"]),
                        };
                        weather.Recommendations = GetWeatherRecommendations(weather);

                        weathers.Add(weather);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return weathers;
        }
        /// <summary>
        /// Get personal weather recommendations based on daily forecast conditions
        /// </summary>
        /// <param name="weather">weather object contains single day weather data</param>
        /// <returns>List<string> all recommendations for a single day</string></returns>
        private List<string> GetWeatherRecommendations( Weather weather )
        {
            List<string> result = new List<string>();

            if (weather.Forecast == "snow") { result.Add("Pack snowshoes!"); }
            if (weather.Forecast == "rain") { result.Add("Pack rain gear and wear waterproof shoes!"); }
            if (weather.Forecast =="thunderstorms")
            {
                result.Add("Seek shelter!");
                result.Add("Avoid hiking on exposed ridges!");
            }
            if (weather.Forecast == "sunny") { result.Add("Pack sunblock"); }
            if (weather.High > 75) { result.Add("Bring and extra gallon of water!"); }
            if ((weather.High - weather.Low) > 20) { result.Add("Wear breathable layers!"); }
            if (weather.Low < 20) { result.Add("Beware of exposure to frigid temperatures"); }
            return result;
        }
        #endregion

    }
}
