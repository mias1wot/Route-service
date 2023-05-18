using Microsoft.EntityFrameworkCore;
using RouteServiceAPP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RouteServiceAPP.Infrastructure.Repos
{
	public class RouteRepo: BaseRepo<Route>, IRouteRepo
	{
		public RouteRepo(RouteServiceContext context): base(context)
		{
		}

		public async Task<IEnumerable<Route>> GetRoutesWithLocalitiesAsync()
		{
			return await _table
				.Include(route => route.RouteLocalities).ThenInclude(routeLocality => routeLocality.Locality).ToListAsync();
		}

		public async Task<Route> GetRouteWithRouteLocalitiesAsync(int routeId)
		{
			return await _table
				.Include(route => route.RouteLocalities)
				.FirstOrDefaultAsync(route => route.RouteId == routeId);
		}

		public async Task<IEnumerable<Route>> GetRoutesAsync()
		{
			return await _table
				.Include(route => route.RouteLocalities).ThenInclude(routeLocality => routeLocality.Locality)
				.Include(route => route.BookedRoutes).ThenInclude(bookedRoute => bookedRoute.Seats)
				.Include(route => route.BookedRoutes).ThenInclude(route => route.From)
				.Include(route => route.BookedRoutes).ThenInclude(route => route.To)
				.ToListAsync();
		}


		public async Task<Route> GetRouteWithLocalitiesAndBoookedRoutesAsync(int id)
		{
			return await _table.Include(route => route.RouteLocalities).ThenInclude(routeLoc => routeLoc.Locality)
				.Include(route => route.BookedRoutes).ThenInclude(bookedRoute => bookedRoute.Seats)
				.Include(route => route.BookedRoutes).ThenInclude(route => route.From)
				.Include(route => route.BookedRoutes).ThenInclude(route => route.To)
				.SingleOrDefaultAsync(route => route.RouteId == id);
		}
	}
}
