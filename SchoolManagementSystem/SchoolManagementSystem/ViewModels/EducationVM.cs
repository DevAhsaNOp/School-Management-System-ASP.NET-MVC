using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.ViewModels
{
    public class EducationVM
    {
        public string IDEdu { get; set; }

        [Required(ErrorMessage = "Please enter Your Institute University")]
        public string InstituteUniversity { get; set; }

        [Required(ErrorMessage = "Please enter Your Title Of Diploma")]
        public string TitleOfDiploma { get; set; }

        [Required(ErrorMessage = "Please enter Your Degree")]
        public string Degree { get; set; }

        [Required(ErrorMessage = "Please enter Start Year")]
        public Nullable<System.DateTime> FromYear { get; set; }

        [Required(ErrorMessage = "Please enter End Year")]
        public Nullable<System.DateTime> ToYear { get; set; }

        [Required(ErrorMessage = "Please enter Your City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter Your Country")]
        public string Country { get; set; }

        public List<SelectListItem> ListOfCountry { get; set; }
        public List<SelectListItem> ListOfCity { get; set; }

    }
}