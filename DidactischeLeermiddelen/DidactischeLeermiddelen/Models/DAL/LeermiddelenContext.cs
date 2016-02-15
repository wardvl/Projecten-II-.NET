﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using DidactischeLeermiddelen.Models.Domain;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DidactischeLeermiddelen.Models.DAL
{
    public class LeermiddelenContext : IdentityDbContext<ApplicationUser>
    {
        #region Constructor
        public LeermiddelenContext() : base("Leermiddelen")
        {

        }
        #endregion

        #region Properties | DbSets
        public DbSet<Customer> Customers { get; set; }

        #endregion

        #region Methods
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }
        public static LeermiddelenContext Create()
        {
            return DependencyResolver.Current.GetService<LeermiddelenContext>();
        } 
        #endregion
    }
}