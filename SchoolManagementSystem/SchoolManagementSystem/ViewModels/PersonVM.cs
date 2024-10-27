using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.ViewModels
{
    public class PersonVM
    {
        public int IDPers { get; set; }

        [Required(ErrorMessage = "Please enter Your First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter Your Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter Your DOB")]
        public Nullable<System.DateTime> DOB { get; set; }

        [Required(ErrorMessage = "Please select Your Nationality")]
        public string Nationality { get; set; }

        [Required(ErrorMessage = "Please select Your Education Level")]
        public string EduLevel { get; set; }

        [Required(ErrorMessage = "Please enter Your Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter Your Telephone Number")]
        public string Tele { get; set; }

        [Required(ErrorMessage = "Please enter Your Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter Your Summary")]
        [DataType(DataType.MultilineText)]
        public string Summary { get; set; }

        [Required(ErrorMessage = "Please enter Your Linkedln Profile")]
        [DataType(DataType.Url)]

        public byte[] Profil { get; set; }

        public List<SelectListItem> ListOfNationality { get; set; }
        public List<SelectListItem> ListOfEduLevel { get; set; }


    }
}