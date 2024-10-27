using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.ViewModels
{
    public class LanguageVM
    {
        public string IDLang { get; set; }

        [Required(ErrorMessage = "Please enter Your Language")]
        public string LanguageName { get; set; }

        [Required(ErrorMessage = "Please select Your Proficiency")]
        public string Proficiency { get; set; }

        public List<SelectListItem> ListOfProficiency { get; set; }

    }
}