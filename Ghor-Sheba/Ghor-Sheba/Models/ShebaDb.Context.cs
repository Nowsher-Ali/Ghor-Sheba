﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ghor_Sheba.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ShebaDbEntities : DbContext
    {
        public ShebaDbEntities()
            : base("name=ShebaDbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Booking_confirms> Booking_confirms { get; set; }
        public DbSet<Booking_details> Booking_details { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<LoginUser> LoginUsers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<service_provider_status> service_provider_status { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}
