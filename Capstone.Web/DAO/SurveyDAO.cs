using NPGeek.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace NPGeek.Web.DAO
{

    public class SurveyDAO : ISurveyDAO
    {
        #region Member Variables
        /// <summary>
        /// local storage for database connection parameters
        /// </summary>
        private readonly string _connectionString;
        #endregion

        #region Constructor
        public SurveyDAO (string connectionstring)
        {
            _connectionString = connectionstring;
        }
        #endregion

        #region public Methods
        /// <summary>
        /// Insert survey answers into survey_results table in database
        /// </summary>
        /// <param name="survey">Survey object to insert in survey_results table</param>
        /// <returns>bool status of survey insert operation</returns>
        public bool SaveNewSurvey(Survey survey)
        {
            bool result = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO survey_result VALUES (@parkCode, @emailAddress, @state, @activityLevel);", conn);
                    cmd.Parameters.AddWithValue("@parkCode", survey.ParkCode);
                    cmd.Parameters.AddWithValue("@emailAddress", survey.EmailAddress);
                    cmd.Parameters.AddWithValue("@state", survey.State);
                    cmd.Parameters.AddWithValue("@activityLevel", survey.ActivityLevel);
                    cmd.ExecuteNonQuery();
                }

                result = true;
            }
            catch (SqlException ex)
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// Retrieve survey results from database, sorted by popularity descending
        /// </summary>
        /// <returns>IList<SurveyResultsViewModel></returns>
        public IList<SurveyResultsViewModel> GetSurveyResults()
        {

            IList<SurveyResultsViewModel> surveyResults = new List<SurveyResultsViewModel>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT park.parkName, survey_result.parkCode, Count(survey_result.parkCode) as votes FROM survey_result " + 
                                                    "JOIN park ON survey_result.parkCode = park.parkCode " +
                                                    "GROUP BY survey_result.parkCode, park.parkName ORDER BY votes DESC, park.parkName ASC", conn);

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var result = new SurveyResultsViewModel()
                        {
                            ParkCode = Convert.ToString(reader["parkCode"]),
                            ParkName = Convert.ToString(reader["parkName"]),
                            Votes = Convert.ToInt32(reader["votes"]),
                        };

                        surveyResults.Add(result);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return surveyResults;
        }

        /// <summary>
        /// Get list of all states from database state code table
        /// </summary>
        /// <returns>List<SelectListItem> list of states and associated key values</returns>
        public List<SelectListItem> GetAllStates()
        {
            List<SelectListItem> stateList = new List<SelectListItem>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * from states ", conn);

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var result = new SelectListItem();
                        result.Value = Convert.ToString(reader["id"]);
                        result.Text = Convert.ToString(reader["name"]);
                        stateList.Add(result);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return stateList;
        }

        /// <summary>
        /// Get list of Park names from database park table
        /// </summary>
        /// <returns>List<SelectListItem> list of parks names and associated code values</returns>
        public List<SelectListItem> GetAllParkNames()
        {
            List<SelectListItem> parkList = new List<SelectListItem>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT parkName,parkCode from park;", conn);

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var result = new SelectListItem();
                        result.Value = Convert.ToString(reader["parkName"]);
                        result.Text = Convert.ToString(reader["parkCode"]);
                        parkList.Add(result);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return parkList;
        }
        #endregion
    }
}
