using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using tikshoret_project.Models;

namespace tikshoret_project.Dal
{
    public class AdminDal : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AdminBAL>().ToTable("dbo.Admin");
        }
        public DbSet<AdminBAL> Admins { get; set; }
    }
}
        