using SchoolManagementSystem.Helper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SchoolManagementSystem.Request
{
    public sealed class ClassSubjectCreateRequest
    {
        public int ClassSubjectID { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a class section")]
        [Required(ErrorMessage = "*")]
        public int ClassSectionID { get; set; }

        [RequireAtLeastOneItem]
        public List<SelectListItem> SubjectID { get; set; }

        public string Title { get; set; }

        [Required(ErrorMessage = "*")]
        public bool IsActive { get; set; }
    }

    public sealed class ClassSubjectUpdateRequest
    {
        public int ClassSubjectID { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a class section")]
        [Required(ErrorMessage = "*")]
        public int ClassSectionID { get; set; }

        [RequireAtLeastOneItem]
        public List<SelectListItem> SubjectID { get; set; }

        public string Title { get; set; }

        [Required(ErrorMessage = "*")]
        public bool IsActive { get; set; }
    }
}