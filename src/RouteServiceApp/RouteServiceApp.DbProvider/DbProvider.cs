using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace RouteServiceApp.DbProviderConfiguration
{
	public class DbProvider
	{
		private readonly IHostEnvironment _environment;
		private readonly IConfiguration _configuration;

		// Fetch connection strings by these names
		static readonly string MsSql = "MsSql";
		static readonly string MsSqlAssembly = typeof(MsSqlMigrations.Marker).Assembly.GetName().Name;

		static readonly string Sqlite = "Sqlite";
		static readonly string SqliteAssembly = typeof(SqliteMigrations.Marker).Assembly.GetName().Name;

		public DbProvider(IHostEnvironment environment, IConfiguration configuration)
		{
			_environment = environment;
			_configuration = configuration;
		}

		// You need to install Microsoft.EntityFrameworkCore.SqlServer (5.0.0 version for Core 3.1) for UseSqlServer extension method
		// MigrationsAssembly - Set the migration folder for DB
		public void Configure(DbContextOptionsBuilder optionsBuilder)
		{
			if (_environment.IsDevelopment())
			{
				optionsBuilder.UseSqlServer(_configuration.GetConnectionString("RouteService"), x => x.MigrationsAssembly(MsSqlAssembly));

			}
			else if (_environment.IsProduction())
			{
				optionsBuilder.UseSqlite(_configuration.GetConnectionString("RouteService"), x => x.MigrationsAssembly(SqliteAssembly));
			}

			// This works as well
			//if (_environment.EnvironmentName == "Development")
		}
	}
}
