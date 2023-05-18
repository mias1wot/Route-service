using Microsoft.AspNetCore.Hosting;
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


			if (reinitializeDbData)
			{
				//Drop create DB and initialize it with data
				using (var scope = webHost.Services.CreateScope())
				{
					var services = scope.ServiceProvider;
					var context = services.GetRequiredService<RouteServiceContext>();
					//RouteServiceDbInitializer.RecreateDatabase(context);
					RouteServiceDbInitializer.ClearData(context);
					RouteServiceDbInitializer.InitializeData(context);
				}
			}

			
			webHost.Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
