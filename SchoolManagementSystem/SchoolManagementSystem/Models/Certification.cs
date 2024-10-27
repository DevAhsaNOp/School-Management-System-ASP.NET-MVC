using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagementSystem.Models
{
    public class Certification
    {
        [Key]
        public int IdCer { get; set; }
        public string CertificationName { get; set; }
        public string CertificationAuthority { get; set; }
        public string LevelCertification { get; set; }
        public Nullable<System.DateTime> FromYear { get; set; }
        public Nullable<int> IdPers { get; set; }

        public virtual Person Person { get; set; }
    }
}