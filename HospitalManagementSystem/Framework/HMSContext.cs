using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Framework
{
    public class HMSContext : DbContext
    {
        public HMSContext() : base("HMSContext")
        {
            Database.SetInitializer<HMSContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.HasDefaultSchema("HMS");
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuRoleMap> MenuRoles { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}