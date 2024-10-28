using System.Collections.Generic;

namespace SchoolManagementSystem.ViewModels
{
    public sealed class ClassSubjectVM
    {
        public int ClassSectionID { get; set; }
        public string Title { get; set; }
        public List<string> Subjects { get; set; }
        public bool IsActive { get; set; }
    }
}