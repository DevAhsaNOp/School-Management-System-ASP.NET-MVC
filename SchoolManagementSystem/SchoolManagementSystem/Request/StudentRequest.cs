using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Request
{
    public sealed class StudentRequest
    {
        public int StudentID { get; set; }

        public int User_ID { get; set; }

        [Display(Name = "Class")]
        [Required(ErrorMessage = "*")]
        [Range(1, int.MaxValue, ErrorMessage = "Please Select Class")]
        public int? ClassID { get; set; }

        [Display(Name = "Section")]
        [Required(ErrorMessage = "*")]
        [Range(1, int.MaxValue, ErrorMessage = "*")]
        public int? SectionID { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "*")]
        public string Name { get; set; }

        [Display(Name = "Father Name")]
        [Required(ErrorMessage = "*")]
        public string FatherName { get; set; }

        [Display(Name = "Date Of Birth")]
        [Required(ErrorMessage = "*")]
        public string DateofBirth { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage = "*")]
        public string Gender { get; set; }

        [Display(Name = "Contact No")]
        [Required(ErrorMessage = "*")]
        public string ContactNo { get; set; }

        [Display(Name = "Guardian Contact No")]
        [Required(ErrorMessage = "*")]
        public string GuardianContactNo { get; set; }

        [Display(Name = "Photo")]
        [Required(ErrorMessage = "*")]
        public string Photo { get; set; }

        [Display(Name = "Addmission Date")]
        [Required(ErrorMessage = "*")]
        public string AddmissionDate { get; set; }

        [Display(Name = "Previous School")]
        public string PreviousSchool { get; set; }

        [Display(Name = "Previous Class Percentage")]
        public double? PreviousPercentage { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "*")]
        public string EmailAddress { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "*")]
        public string Address { get; set; }

        [Display(Name = "Nationality")]
        [Required(ErrorMessage = "*")]
        public string Nationality { get; set; }
    }
}