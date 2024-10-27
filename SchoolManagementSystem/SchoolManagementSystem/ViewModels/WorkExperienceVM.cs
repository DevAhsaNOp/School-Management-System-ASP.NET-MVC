using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.ViewModels
{
    public class WorkExperienceVM
    {
        public int IDExp { get; set; }

        [Required(ErrorMessage = "Please enter Your Company")]
        public string Company { get; set; }
        [Required(ErrorMessage = "Please enter Your Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please select Your Country")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Please select Start Year")]
        public Nullable<System.DateTime> FromYear { get; set; }
        [Required(ErrorMessage = "Please select End Year")]
        public Nullable<System.DateTime> ToYear { get; set; }
        [Required(ErrorMessage = "Please enter Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public List<SelectListItem> ListOfCountry { get; set; }


    }
}