
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace BusinessLogicLayer.Models
{

using System;
    using System.Collections.Generic;
    
public partial class EnetInterventionType
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public EnetInterventionType()
    {

        this.EnetInterventions = new HashSet<EnetIntervention>();

    }


    public int InterventionTypeId { get; set; }

    public string Name { get; set; }

    public Nullable<int> LabourHours { get; set; }

    public Nullable<decimal> Cost { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<EnetIntervention> EnetInterventions { get; set; }

}

}
