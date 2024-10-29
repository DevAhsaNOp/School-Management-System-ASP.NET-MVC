using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Response
{
    public class ClassSubjectResponse
    {
        [Display(Name = "Class Name")]
        public string ClassName { get; set; }

        [Display(Name = "Section")]
        public string Section { get; set; }

        [Display(Name = "Subjects")]
        public List<string> Subjects { get; set; }

        public int ClassSectionID { get; set; }
    }
}