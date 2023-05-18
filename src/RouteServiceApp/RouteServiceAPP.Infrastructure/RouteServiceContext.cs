using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RouteServiceAPP.Domain.Entities;
using System;


/*
	For migrations to work you need to install:
	API:
		Microsoft.EntityFrameworkCore.Tools (5.0.17 for .Net Core 3.1)
	Infrastructure:
		Microsoft.EntityFrameworkCore.Tools (5.0.17)
		Microsoft.EntityFrameworkCore version (5.0.17)


    Add migration:
Add-migration -Project RouteServiceApp.Infrastructure -StartupProject RouteServiceApp.API MigName

Update-database -Project RouteServiceApp.Infrastructure -StartupProject RouteServiceApp.API

    Rollback to MigName migration:
Update-Database -Project RouteServiceApp.Infrastructure -StartupProject RouteServiceApp.API MigName

    Remove last migration:
Remove-migration -Project RouteServiceApp.Infrastructure -StartupProject RouteServiceApp.API
*/

namespace RouteServiceAPP.Infrastructure
{
	public class RouteServiceContext : DbContext
	{
		private readonly IConfiguration _configuration;
		public RouteServiceContext(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public RouteServiceContext(DbContextOptions<RouteServiceContext> options, IConfiguration configuration) : base(options)
		{
			_configuration = configuration;
		}

		public virtual DbSet<Locality> Localities { get; set; }
		public virtual DbSet<Route> Routes { get; set; }
		public virtual DbSet<RouteLocality> RouteLocalities { get; set; }
		public virtual DbSet<BookedRoute> BookedRoutes { get; set; }
		public virtual DbSet<Seat> Seats { get; set; }


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				// You need to install Microsoft.EntityFrameworkCore.SqlServer (5.0.0 version for Core 3.1) for UseSqlServer extension method
				optionsBuilder.UseSqlServer(_configuration.GetConnectionString("RouteService"));
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

			modelBuilder.Entity<RouteLocality>(entity =>
			{
				entity.HasKey(e => new { e.RouteId, e.OrdinalNumber });
					//.HasName("PK_dbo.AutoOperationFile");
			});

			modelBuilder.Entity<Seat>(entity =>
			{
				entity.HasKey(e => new { e.BookedRouteId, e.Number });
				//.HasName("PK_dbo.AutoOperationFile");
			});
		}
	}
}
