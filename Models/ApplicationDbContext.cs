using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DataStruct.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, RoleIntPk, int,
       UserLoginIntPk, UserRoleIntPk, UserClaimIntPk>
    {
        public ApplicationDbContext()
            : base("name=DSA_db")
        {
             Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext,Migrations.Configuration>());

         
        }

        //Create Database Tables
        public DbSet<Exams> Exams { get; set; }
        public DbSet<Lectures> Lectures { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<Answers> Answers { get; set; }
        public DbSet<ExamStats> ExamStats { get; set; }
        public DbSet<Cart>Carts { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Logger> Loggers { get; set; }
        public DbSet<Charges> Charges { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

         //Fluent Api Configurations
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<RoleIntPk>().ToTable("Roles");
            modelBuilder.Entity<UserClaimIntPk>().ToTable("UserClaims");
            modelBuilder.Entity<UserLoginIntPk>().ToTable("UserLogins");
            modelBuilder.Entity<UserRoleIntPk>().ToTable("UserRoles");

          
            
        }
    }


}

