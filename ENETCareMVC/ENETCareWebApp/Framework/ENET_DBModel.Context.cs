﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ENETCareEntities : DbContext
    {
        public ENETCareEntities()
            : base("name=ENETCareEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ENET_Client> ENET_Client { get; set; }
        public virtual DbSet<ENET_District> ENET_District { get; set; }
        public virtual DbSet<ENET_Intervention> ENET_Intervention { get; set; }
        public virtual DbSet<ENET_InterventionType> ENET_InterventionType { get; set; }
        public virtual DbSet<ENET_Manager> ENET_Manager { get; set; }
        public virtual DbSet<ENET_SiteEngineer> ENET_SiteEngineer { get; set; }
        public virtual DbSet<ENET_State> ENET_State { get; set; }
        public virtual DbSet<ENET_User> ENET_User { get; set; }
    }
}
