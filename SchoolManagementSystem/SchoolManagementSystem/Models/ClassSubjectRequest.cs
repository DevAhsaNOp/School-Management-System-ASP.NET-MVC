using System.Collections.Generic;

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

    public sealed class ClassSubjectUpdateteRequest
    {
        public int ClassSubjectID { get; set; }

        public int ClassSectionID { get; set; }

        public List<int> SubjectID { get; set; }

        public string Title { get; set; }

        public bool IsActive { get; set; }
    }
}