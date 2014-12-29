using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
	internal sealed class Configuration : DbMigrationsConfiguration<ApplicationContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;

			// add this
			SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
		}

		protected override void Seed(ApplicationContext context)
		{
			base.Seed(context);

			if (context.Users.Where(x => x.UserName == "admin").ToList().Count == 0)
			{
				PasswordHasher ph = new PasswordHasher();
				context.Users.Add(new ApplicationUser { UserName = "polaris878@gmail.com", LockoutEnabled = true, SecurityStamp = Guid.NewGuid().ToString(), PasswordHash = ph.HashPassword("admin"), Email = "polaris878@gmail.com", EmailConfirmed = true });
				context.SaveChanges();
			}
		}

		/* ... */
	}
}