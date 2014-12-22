using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using MySql.Data.Entity;
    using System.Data.Entity;
    
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// To use this constructor you have to have a connection string 
        /// name "MyConnection" in your configuration
        /// </summary>
		public ApplicationContext() : this("DefaultConnection") { }
 
        /// <summary>
        /// Construct a db context
        /// </summary>
        /// <param name="connStringName">Connection to use for the database</param>
        public ApplicationContext(string connStringName) : base(connStringName) { }
 
        /// <summary>
        /// Some database fixup / model constraints
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
 
            #region Fix asp.net identity 2.0 tables under MySQL
            // Explanations: primary keys can easily get too long for MySQL's 
            // (InnoDB's) stupid 767 bytes limit.
            // With the two following lines we rewrite the generation to keep
            // those columns "short" enough
            modelBuilder.Entity<IdentityRole>()
                .Property(c => c.Name)
                .HasMaxLength(128)
                .IsRequired();
 
            // We have to declare the table name here, otherwise IdentityUser 
            // will be created
            modelBuilder.Entity<IdentityUser>()
                .ToTable("AspNetUsers")
                .Property(c => c.UserName)
                .HasMaxLength(128)
                .IsRequired();
            #endregion
        }

		public static ApplicationContext Create()
		{
				return new ApplicationContext();
		}
    }
}