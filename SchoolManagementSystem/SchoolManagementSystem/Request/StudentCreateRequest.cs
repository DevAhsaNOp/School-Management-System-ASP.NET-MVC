using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Request
{
    public sealed class StudentCreateRequest
    {
        public int StudentID { get; set; }

        [Required(ErrorMessage = "*")]
        public int User_ID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Class")]
        [Range(1, int.MaxValue, ErrorMessage = "Please Select Class")]
        public int? ClassID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Section")]
        [Range(1, int.MaxValue, ErrorMessage = "Please Select Section")]
        public int? SectionID { get; set; }

        [Required(ErrorMessage = "*")]
        public string Name { get; set; }

        [Required(ErrorMessage = "*")]
        public string FatherName { get; set; }

        [Required(ErrorMessage = "*")]
        public DateTime DateofBirth { get; set; }

        [Required(ErrorMessage = "*")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "*")]
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "*")]
        public string GuardianContactNo { get; set; }

        [Required(ErrorMessage = "*")]
        public string Photo { get; set; }

        [Required(ErrorMessage = "*")]
        public DateTime AddmissionDate { get; set; }

        [Required(ErrorMessage = "*")]
        public string PreviousSchool { get; set; }

        [Required(ErrorMessage = "*")]
        public double? PreviousPercentage { get; set; }

        [Required(ErrorMessage = "*")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "*")]
        public string Address { get; set; }

        [Required(ErrorMessage = "*")]
        public string Nationality { get; set; }
    }
}