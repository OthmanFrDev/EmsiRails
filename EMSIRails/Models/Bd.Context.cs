﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EMSIRails.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ExpresstrainEntities : DbContext
    {
        public ExpresstrainEntities()
            : base("name=ExpresstrainEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<gare> gares { get; set; }
        public virtual DbSet<infosarret> infosarrets { get; set; }
        public virtual DbSet<retard> retards { get; set; }
        public virtual DbSet<train> trains { get; set; }
        public virtual DbSet<ville> villes { get; set; }
        public virtual DbSet<voyage> voyages { get; set; }
    }
}
