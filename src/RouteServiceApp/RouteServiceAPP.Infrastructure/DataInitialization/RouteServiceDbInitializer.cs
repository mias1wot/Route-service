using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RouteServiceAPP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace RouteServiceAPP.Infrastructure.DataInitialization
{
	public static class RouteServiceDbInitializer
	{
		public static void InitializeData(RouteServiceContext context)
		{
			var localities = new List<Locality>
			{
				new Locality { Name = "Львів" },
				new Locality { Name = "Луцьк" },
				new Locality { Name = "Рівне" },
				new Locality { Name = "Звягель" },
				new Locality { Name = "Житомир" },
				new Locality { Name = "Ірпінь" },
				new Locality { Name = "Київ" },
				new Locality { Name = "Біла Церква" },
				new Locality { Name = "Вінниця" },
				new Locality { Name = "Хмельницький" },
				new Locality { Name = "Тернопіль" },
				new Locality { Name = "Стрий" },
			};
			context.Localities.AddOrUpdate(locality => locality.Name, localities);
			context.SaveChanges();

			var routes = new List<Route>
			{
				new Route
				{
					ExtraInfo = "Це буде класно! Подорож на 20 хвилин.",
					SeatStartNumber = 10,
					SeatEndNumber = 15,
					RouteLocalities = new List<RouteLocality>
					{
						new RouteLocality
						{
							OrdinalNumber = 1,
							DepartureTime = new DateTime(2023, 6, 18, 20, 45, 0),
							ArrivalTime = new DateTime(2023, 6, 18, 20, 30, 0),
							Locality = context.Localities.Find("Львів")
						},
						new RouteLocality
						{
							OrdinalNumber = 2,
							DepartureTime = new DateTime(2023, 6, 18, 23, 30, 0),
							ArrivalTime = new DateTime(2023, 6, 18, 20, 55, 0),
							Locality = context.Localities.Find("Тернопіль")
						},
						new RouteLocality
						{
							OrdinalNumber = 3,
							DepartureTime = new DateTime(2023, 6, 18, 12, 30, 0),
							ArrivalTime = new DateTime(2023, 6, 18, 5, 45, 0),
							Locality = context.Localities.Find("Київ")
						}
					}
				}
			};
			context.Routes.AddOrUpdate(route => route.RouteId, routes);
			context.SaveChanges();
		}

		public static void ClearData(RouteServiceContext context)
		{
			ExecuteDeleteSql(context, nameof(RouteServiceContext.Seats));
			ExecuteDeleteSql(context, nameof(RouteServiceContext.Routes));
			ExecuteDeleteSql(context, nameof(RouteServiceContext.RouteLocalities));
			ExecuteDeleteSql(context, nameof(RouteServiceContext.BookedRoutes));
			ExecuteDeleteSql(context, nameof(RouteServiceContext.Localities));

			ResetIdentity(context);
		}
		private static void ExecuteDeleteSql(RouteServiceContext context, string tableName)
		{
			var rawSqlString = $"Delete from dbo.{tableName}";
			context.Database.ExecuteSqlRaw(rawSqlString);
		}
		private static void ResetIdentity(RouteServiceContext context)
		{
			var tables = new[] { nameof(RouteServiceContext.BookedRoutes), nameof(RouteServiceContext.Routes) };
			foreach (var table in tables)
			{
				var rawSqlString = $"DBCC CHECKIDENT (\"dbo.{table}\", RESEED, 0);";
				context.Database.ExecuteSqlRaw(rawSqlString);
			}
		}

		public static void RecreateDatabase(RouteServiceContext context)
		{
			context.Database.EnsureDeleted();
			context.Database.Migrate();
		}
	}


	public static class DbSetExtensions
	{
		public static void AddOrUpdate<T>(this DbSet<T> dbSet, Expression<Func<T, object>> identifierExpression, IEnumerable<T> entities)
			where T : class
		{
			var identifierFunc = identifierExpression.Compile();
			var existingEntities = dbSet.ToDictionary(entity => identifierFunc(entity));

			foreach (var entity in entities)
			{
				var identifier = identifierFunc(entity);
				if (existingEntities.TryGetValue(identifier, out var existingEntity))
				{
					dbSet.Update(existingEntity);

					var dbContext = dbSet.GetService<RouteServiceContext>();
					// Copy the property values from the input entity to the existing entity
					dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
				}
				else
				{
					dbSet.Add(entity);
				}
			}
		}
	}
}
