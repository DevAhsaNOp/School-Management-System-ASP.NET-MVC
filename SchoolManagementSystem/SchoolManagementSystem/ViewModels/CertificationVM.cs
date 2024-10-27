using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.ViewModels
{
    public class CertificationVM
    {
        [Required(ErrorMessage ="Please enter Your Certification Name")]
        public string CertificationName { get; set; }

        [Required(ErrorMessage = "Please enter Your Certification Authority")]
        public string CertificationAuthority { get; set; }

        [Required(ErrorMessage = "Please enter Your Certification Level")]
        public string CertificationLevel { get; set; }

        [Required(ErrorMessage = "Please select Your Achivement Date")]
        public Nullable<System.DateTime> FromYear { get; set; }

        public List<SelectListItem> ListOfLevel { get; set; }


    }
}