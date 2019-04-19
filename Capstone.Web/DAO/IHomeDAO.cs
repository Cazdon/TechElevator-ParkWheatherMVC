using NPGeek.Web.Models;
using System.Collections.Generic;

namespace NPGeek.Web.DAO
{
    /// <summary>
    /// Interface class definition for Home data access
    /// </summary>
    public interface IHomeDAO
    {
        IList<HomeViewModel> GetAllParks();

        HomeDetailViewModel GetDetailViewModel(string pCode);
    }
}
