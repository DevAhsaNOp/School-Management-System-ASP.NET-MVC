//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatabaseAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmployeeSalaryTable
    {
        public int EmployeeSalaryID { get; set; }
        public int UserID { get; set; }
        public int StaffID { get; set; }
        public string SalaryMonth { get; set; }
        public string SalaryYear { get; set; }
        public System.DateTime SalaryDate { get; set; }
        public string Comments { get; set; }
        public double Salary { get; set; }
    
        public virtual StaffTable StaffTable { get; set; }
        public virtual UserTable UserTable { get; set; }
    }
}
