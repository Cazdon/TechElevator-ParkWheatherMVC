using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NPGeek.Web.Models;
using NPGeek.Web.DAO;
using SessionControllerData;

namespace NPGeek.Web.Controllers
{
    public class HomeController : SessionController
    {
        #region Member Variables
        /// <summary>
        /// local storage for database connection parameters
        /// </summary>
        private IHomeDAO _homeDao;
        #endregion

        #region Constructor
        /// <summary>
        /// Home Controller Constructor
        /// </summary>
        /// <param name="homeDao">DAO for Home Controller Actions</param>
        public HomeController (IHomeDAO homeDao)
        {
            _homeDao = homeDao;
        }
        #endregion

        #region public Methods
        /// <summary>
        /// Home / Index (default) action Display lost of all national parks in database
        /// </summary>
        /// <returns>IActionResult</returns>
        public IActionResult Index()
        {
            var homeVM = _homeDao.GetAllParks();
            return View(homeVM);
        }

        /// <summary>
        /// Home / Detail action Display detailed park information with 5-day weather forecast
        /// </summary>
        /// <param name="parkcode">Park code identifier</param>
        /// <returns>IActionResult</returns>
        public IActionResult Detail(string parkcode)
        {
            HomeDetailViewModel homeDetailVM = _homeDao.GetDetailViewModel(parkcode);

            return View(homeDetailVM);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion
    }
}
