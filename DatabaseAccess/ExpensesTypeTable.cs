
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
    
public partial class ExpensesTypeTable
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public ExpensesTypeTable()
    {

        this.ExpensesTables = new HashSet<ExpensesTable>();

    }


    public int ExpenseTypeID { get; set; }

    public int User_ID { get; set; }

    public string Name { get; set; }

    public bool IsActive { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<ExpensesTable> ExpensesTables { get; set; }

    public virtual UserTable UserTable { get; set; }

}

}
