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
    
    public partial class ClassSectionTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClassSectionTable()
        {
            this.ClassSubjectTables = new HashSet<ClassSubjectTable>();
        }
    
        public int ClassSectionID { get; set; }
        public int ClassID { get; set; }
        public int SectionID { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
    
        public virtual ClassTable ClassTable { get; set; }
        public virtual SectionTable SectionTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClassSubjectTable> ClassSubjectTables { get; set; }
    }
}
