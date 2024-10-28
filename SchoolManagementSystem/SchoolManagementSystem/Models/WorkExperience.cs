using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class WorkExperience
    {
        [Key]
        public int IDExp { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }
        public string Country { get; set; }
        public Nullable<System.DateTime> FromYear { get; set; }
        public Nullable<System.DateTime> ToYear { get; set; }
        public string Description { get; set; }
        public Nullable<int> IdPers { get; set; }

        public virtual Person Person { get; set; }

    }
}