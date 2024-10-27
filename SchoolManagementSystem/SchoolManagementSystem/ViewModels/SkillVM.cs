using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagementSystem.ViewModels
{
    public class SkillVM
    {
        public int IDSki { get; set; }

        [Required(ErrorMessage = "Please enter Your Skill")]
        public string SkillName { get; set; }

    }
}