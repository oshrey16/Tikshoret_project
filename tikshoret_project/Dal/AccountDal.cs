using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using tikshoret_project.Models;

namespace tikshoret_project.Dal
{
    public class AccountDal : DbContext
    {
        public static string connetionString = "Data Source=OSHREYDELL5590;Initial Catalog=NetworkingProject;Integrated Security=True";
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AccountBAL>().ToTable("dbo.Customer");
            modelBuilder.Entity<AdminBAL>().ToTable("dbo.Admin");
            modelBuilder.Entity<HallBAL>().ToTable("dbo.Halls");
            modelBuilder.Entity<MoviesLibBAL>().ToTable("dbo.MoviesLib");
            modelBuilder.Entity<MovieShowBal>().ToTable("dbo.Movies");
            modelBuilder.Entity<TicketsBal>().ToTable("dbo.Tickets");
            modelBuilder.Entity<PaidTicketsBal>().ToTable("dbo.PaidTickets");
        }
        public DbSet<AccountBAL> Accounts { get; set; }
        public DbSet<AdminBAL> Admins { get; set; }
        public DbSet<HallBAL> Halls { get; set; }
        public DbSet<MoviesLibBAL> MoviesLib { get; set; }
        public DbSet<MovieShowBal> Movies { get; set; }
        public DbSet<TicketsBal> Tickets { get; set; }
        public DbSet<PaidTicketsBal> PaidTickets { get; set; }
    }
}