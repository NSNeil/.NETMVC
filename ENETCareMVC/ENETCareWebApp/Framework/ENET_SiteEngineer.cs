//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ENETCare.IMS.WebApp.Framework
{
    using System;
    using System.Collections.Generic;
    
    public partial class ENET_SiteEngineer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ENET_SiteEngineer()
        {
            this.ENET_Intervention = new HashSet<ENET_Intervention>();
        }
    
        public int SiteEngineerId { get; set; }
        public int UserId { get; set; }
        public string District { get; set; }
        public decimal MaxCost { get; set; }
        public int MaxHours { get; set; }
    
        public virtual ENET_District ENET_District { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ENET_Intervention> ENET_Intervention { get; set; }
        public virtual ENET_User ENET_User { get; set; }
    }
}