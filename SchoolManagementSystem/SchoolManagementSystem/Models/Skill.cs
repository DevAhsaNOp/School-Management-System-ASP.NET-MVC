using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class Skill
    {
        [Key]
        public int IDSki { get; set; }
        public string SkillName { get; set; }
        public Nullable<int> IdPers { get; set; }

        public virtual Person Person { get; set; }

    }
}