﻿using BO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace TP1_Module_6.Data
{
    public class TpMd6Context : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public TpMd6Context() : base("name=TpMd6Context")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            /*            modelBuilder.Entity<Samourai>().HasOptional(a => a.Arme).WithRequired();*/
            modelBuilder.Entity<Samourai>().HasMany(am => am.ArtMartials).WithMany();
            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<BO.Arme> Armes { get; set; }

        public System.Data.Entity.DbSet<BO.Samourai> Samourais { get; set; }

        public System.Data.Entity.DbSet<BO.ArtMartial> ArtMartials { get; set; }
    }
}
