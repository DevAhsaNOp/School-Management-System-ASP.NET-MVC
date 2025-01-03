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
    
    public partial class ProgrameTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProgrameTable()
        {
            this.AnnualTables = new HashSet<AnnualTable>();
            this.ProgrameSessionTables = new HashSet<ProgrameSessionTable>();
            this.SessionProgrameSubjectSettingTables = new HashSet<SessionProgrameSubjectSettingTable>();
            this.SubmissionFeeTables = new HashSet<SubmissionFeeTable>();
            this.StudentTables = new HashSet<StudentTable>();
        }
    
        public int ProgrameID { get; set; }
        public int User_ID { get; set; }
        public string Name { get; set; }
        public System.DateTime StartDate { get; set; }
        public bool IsActive { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AnnualTable> AnnualTables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProgrameSessionTable> ProgrameSessionTables { get; set; }
        public virtual UserTable UserTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SessionProgrameSubjectSettingTable> SessionProgrameSubjectSettingTables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubmissionFeeTable> SubmissionFeeTables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentTable> StudentTables { get; set; }
    }
}
