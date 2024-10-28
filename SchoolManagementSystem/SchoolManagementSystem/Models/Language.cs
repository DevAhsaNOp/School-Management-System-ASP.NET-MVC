using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class Language
    {
        [Key]
        public int IDLang { get; set; }
        public string LanguageName { get; set; }
        public string Proficiency { get; set; }
        public Nullable<int> IdPers { get; set; }

        public virtual Person Person { get; set; }
    }
}