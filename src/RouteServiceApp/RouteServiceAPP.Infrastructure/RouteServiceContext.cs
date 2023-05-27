using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RouteServiceAPP.Domain.Entities;
using System;


/* Commands in this section are only for 1 DB provider!
	For migrations to work you need to install (only if you don't use Dependency injection from the startup project):
	API:
		Microsoft.EntityFrameworkCore.Tools (5.0.17 for .Net Core 3.1)
	Infrastructure:
		Microsoft.EntityFrameworkCore.Tools (5.0.17)
		Microsoft.EntityFrameworkCore version (5.0.17)


$env:ASPNETCORE_ENVIRONMENT='Development'

    Add migration:
Add-migration -Project RouteServiceApp.Infrastructure -StartupProject RouteServiceApp.API MigName

Update-database -Project RouteServiceApp.Infrastructure -StartupProject RouteServiceApp.API

    Rollback to MigName migration:
Update-Database -Project RouteServiceApp.Infrastructure -StartupProject RouteServiceApp.API MigName

    Remove last migration:
Remove-migration -Project RouteServiceApp.Infrastructure -StartupProject RouteServiceApp.API
*/



// The next 2 sections describe migrations for MS SQL and SQLite providers

/* MS SQL
 * 
 * $env:ASPNETCORE_ENVIRONMENT='Development'
 * 
 * Add-migration -Project MsSqlMigrations -StartupProject RouteServiceApp.API MigName
 * 
 * Remove-migration -Project MsSqlMigrations -StartupProject RouteServiceApp.API
 * 
 */

/* SQLite
 * 
 * $env:ASPNETCORE_ENVIRONMENT='Production'
 * 
 * Add-migration -Project SqliteMigrations -StartupProject RouteServiceApp.API Initial
 * 
 */

namespace RouteServiceAPP.Infrastructure
{
	public class RouteServiceContext : DbContext
	{
		public RouteServiceContext(DbContextOptions<RouteServiceContext> options) : base(options)
		{
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
				throw new NotImplementedException("You must use DbProvider project to configure connection string and migration folders. So just add a reference to API project which delegates the work to DbProvider.");
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
