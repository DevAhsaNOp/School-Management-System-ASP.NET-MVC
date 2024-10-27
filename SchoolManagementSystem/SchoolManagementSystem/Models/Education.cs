    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagementSystem.Models
{
    public class Education
    {
        [Key]
        public int IDEdu { get; set; }
        public string InstituteUniversity { get; set; }
        public string TitleOfDiploma { get; set; }
        public string Degree { get; set; }
        public Nullable<System.DateTime> FromYear { get; set; }
        public Nullable<System.DateTime> ToYear { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public Nullable<int> IdPers { get; set; }

        public virtual Person Person { get; set; }
    }
}