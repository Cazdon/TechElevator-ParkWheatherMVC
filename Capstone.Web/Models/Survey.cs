namespace NPGeek.Web.Models
{
    /// <summary>
    /// Survey class defines data elements in survey_results table in database
    /// </summary>
    public class Survey
    {
        public int SurveyID { get; set; }

        public string ParkCode { get; set; }

        public string EmailAddress { get; set; }

        public int State { get; set; }

        public string ActivityLevel { get; set; }
    }
}
