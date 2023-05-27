using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RouteServiceAPP.Infrastructure;
using RouteServiceAPP.Infrastructure.DataInitialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouteServiceApp.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			bool reinitializeDbData = false;


			var webHost = CreateHostBuilder(args).Build();

			using (var scope = webHost.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				var context = services.GetRequiredService<RouteServiceContext>();

				// Create DB if it doesn't exist
				if (!context.Database.GetService<IRelationalDatabaseCreator>().Exists())
				{
					try
					{
						context.Database.Migrate();
					}
					catch (Exception ex)
					{
						Console.WriteLine($"Migration has failed: {ex.Message}.");
					}
				}


				// Clear and initialize data if required
				// This in only for SQL Server!
				if (reinitializeDbData)
				{
					RouteServiceDbInitializer.ClearData(context);
					//RouteServiceDbInitializer.InitializeTestData(context); // must go after InitializeData() if required.
				}

				// Write initial required values to DB
				RouteServiceDbInitializer.InitializeData(context);
			}


			
			webHost.Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
					webBuilder.UseUrls("http://*:5005");
				});
	}
}
