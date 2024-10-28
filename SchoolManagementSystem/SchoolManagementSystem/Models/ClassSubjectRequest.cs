using System.Collections.Generic;
using System.Web.Mvc;

namespace SchoolManagementSystem.Models
{
    public sealed class ClassSubjectCreateRequest
    {
        public int ClassSubjectID { get; set; }

        public int ClassSectionID { get; set; }

        public List<int> SubjectID { get; set; }

        public string Title { get; set; }

        public bool IsActive { get; set; }
    }

    public sealed class ClassSubjectUpdateRequest
    {
        public int ClassSubjectID { get; set; }
        public int ClassSectionID { get; set; }
        public List<SelectListItem> SubjectID { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
    }

    public sealed class ClassSubjectDetails
    {
        public int ClassSubjectID { get; set; }
        public int SubjectID { get; set; }
        public string Title { get; set; }
    }
}