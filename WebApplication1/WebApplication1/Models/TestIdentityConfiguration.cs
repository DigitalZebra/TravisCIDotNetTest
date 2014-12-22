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

		/* ... */
	}
}