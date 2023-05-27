using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RouteServiceApp.API.AutoMapperProfiles;
using RouteServiceApp.API.Middlewares;
using RouteServiceApp.API.Validators.Locality;
using RouteServiceApp.DbProviderConfiguration;
using RouteServiceAPP.Application;
using RouteServiceAPP.Application.AutoMapperProfiles;
using RouteServiceAPP.Application.Services.Interfaces;
using RouteServiceAPP.Domain.Repos;
using RouteServiceAPP.Infrastructure;
using RouteServiceAPP.Infrastructure.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouteServiceApp.API
{
	public class Startup
	{
		public Startup(IConfiguration configuration, IHostEnvironment hostingEnvironment)
		{
			Configuration = configuration;
			HostingEnvironment = hostingEnvironment;
		}

		public IConfiguration Configuration { get; }
		public IHostEnvironment HostingEnvironment { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			ServicesConfiguration.Initialize(Configuration);

			services.AddSingleton<IHostEnvironment>(HostingEnvironment);

			services.AddControllers();

			services.AddAutoMapper(typeof(ApiProfile), typeof(ApplicationProfile));

			services.AddValidatorsFromAssemblyContaining<LocalityRequestValidator>();

			services.AddSingleton<DbProvider>();
			services.AddDbContext<RouteServiceContext>((serviceProvider, options) =>
			{
				var dbProvider = new DbProvider(HostingEnvironment, Configuration);
				dbProvider.Configure(options);
			});

			services.AddScoped<IUnitOfWork, UnitOfWork>();

			services.AddScoped<IRouteService, RouteService>();
			services.AddScoped<ILocalityService, LocalityService>();

			services.AddSwagger();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseSwagger();
			app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/V1/swagger.json", "RouteServiceApp"));

			app.UseHttpsRedirection();

			app.UseMiddleware<ExceptionHandlerMiddleware>();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
