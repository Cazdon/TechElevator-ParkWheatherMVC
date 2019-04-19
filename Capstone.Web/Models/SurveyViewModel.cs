using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NPGeek.Web.Models
{
    /// <summary>
    /// Survey view model for survey form data display and return
    /// </summary>
    public class SurveyViewModel
    {
        [Display(Name="Favorite National Park")]
        public string ParkCode { get; set; }

        [Display(Name = "Your Email")]
        public string EmailAddress { get; set; }

        [Display(Name = "State of residence")]
        public int State { get; set; }

        [Display(Name = "Activity level")]
        public string ActivityLevel { get; set; }

        public List<SelectListItem> ParkNames { get; set; } = null;
        public List<SelectListItem> StateNames { get; set; } = null;

    }
}
