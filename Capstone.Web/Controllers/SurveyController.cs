using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NPGeek.Web.Models;
using NPGeek.Web.DAO;
using Microsoft.AspNetCore.Mvc.Rendering;
using SessionControllerData;

namespace NPGeek.Web.Controllers
{
    public class SurveyController : SessionController
    {
        #region Member Variables
        /// <summary>
        /// local storage for database connection parameters
        /// </summary>
        private ISurveyDAO _surveyDao;
        private IHomeDAO _parkDao;
        #endregion

        #region Constructor
        /// <summary>
        /// Survey Controller Constructor
        /// </summary>
        /// <param name="surveyDao"></param>
        /// <param name="parkDao"></param>
        public SurveyController ( ISurveyDAO surveyDao , IHomeDAO parkDao)
        {
            _surveyDao = surveyDao;
            _parkDao = parkDao;
        }
        #endregion

        #region public Methods
        /// <summary>
        /// Survey / Index [GET] (default) Display survey form view for data input
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet]
        public IActionResult Index()
        {
            SurveyViewModel surveyVM = new SurveyViewModel();
            IList<HomeViewModel> parks = _parkDao.GetAllParks();
            List<SelectListItem> parknames = new List<SelectListItem> ();

            foreach (HomeViewModel park in parks)
            {
                SelectListItem parkItem = new SelectListItem();
                parkItem.Text = park.ParkName;
                parkItem.Value = park.ParkCode;
                parknames.Add(parkItem);
            }
            surveyVM.ParkNames = parknames;
            surveyVM.StateNames = _surveyDao.GetAllStates();

            return View(surveyVM);
        }

        /// <summary>
        /// Survey / Index / [POST] retrieve survey data and redirect to survey results
        /// </summary>
        /// <param name="surveyVM">Survey View Model caontains survey answers</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        public IActionResult Index(SurveyViewModel surveyVM)
        {
            Survey survey = new Survey();
            survey.ParkCode = surveyVM.ParkCode;
            survey.EmailAddress = surveyVM.EmailAddress;
            survey.State = surveyVM.State;
            survey.ActivityLevel = surveyVM.ActivityLevel;
            bool status = _surveyDao.SaveNewSurvey(survey);

            return RedirectToAction("Results", "Survey");
        }

        /// <summary>
        /// Survey / Results / [GET] Display survey results ranked by popularity
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet]
        public IActionResult Results()
        {
            IList<SurveyResultsViewModel> surveyResultsVM = new List<SurveyResultsViewModel>();
            surveyResultsVM = _surveyDao.GetSurveyResults();
            return View(surveyResultsVM);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion
    }
}
