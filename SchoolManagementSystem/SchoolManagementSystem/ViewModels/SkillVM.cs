using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.ViewModels
{
    public class SkillVM
    {
        public int IDSki { get; set; }

        [Required(ErrorMessage = "Please enter Your Skill")]
        public string SkillName { get; set; }

    }
}