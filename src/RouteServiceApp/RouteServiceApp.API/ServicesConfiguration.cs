using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteServiceApp.API
{
	public static class ServicesConfiguration
	{
		private static IConfiguration _configuration;

		public static void Initialize(IConfiguration configuration)
		{
			_configuration = configuration;
		}


		public static void AddSwagger(this IServiceCollection services)
		{
			// You need to install Swashbuckle.AspNetCore
			// It requires all controllers to have [action] part in [Route("api/[controller]/[action]")] (default is [Route("api/[controller]")])
			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("V1", new OpenApiInfo
				{
					Version = "V1",
					Title = "RouteServiceApp",
					Description = "RouteServiceApp Web Api"
				});
			});
		}
	}
}
