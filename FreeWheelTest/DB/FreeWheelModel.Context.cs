﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FreeWheelTest.DB
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FreeWheelEntities : DbContext
    {
        public FreeWheelEntities()
            : base("name=FreeWheelEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<PROGRAM> PROGRAM { get; set; }
        public virtual DbSet<STATION> STATION { get; set; }
        public virtual DbSet<CELLS> CELLS { get; set; }
        public virtual DbSet<MARKET> MARKET { get; set; }
        public virtual DbSet<MARKET_POP> MARKET_POP { get; set; }
    }
}
