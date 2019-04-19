using Microsoft.AspNetCore.Mvc.Rendering;
using NPGeek.Web.Models;
using System.Collections.Generic;

namespace NPGeek.Web.DAO
{
    /// <summary>
    /// Interface class definition for Survey data access
    /// </summary>
    public interface ISurveyDAO
    {
        bool SaveNewSurvey(Survey survey);

        IList<SurveyResultsViewModel> GetSurveyResults();

        List<SelectListItem> GetAllStates();

        List<SelectListItem> GetAllParkNames();
    }
}
