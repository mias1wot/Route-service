using RouteServiceAPP.Domain.Entities;
using RouteServiceAPP.Domain.Repos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RouteServiceAPP.Infrastructure.Repos
{
	public interface IRouteRepo: IBaseRepo<Route>
	{
		Task<IEnumerable<Route>> GetRoutesWithLocalitiesAsync();
		Task<Route> GetRouteWithRouteLocalitiesAsync(int routeId);
		Task<IEnumerable<Route>> GetRoutesAsync();
		Task<Route> GetRouteWithLocalitiesAndBoookedRoutesAsync(int id);
	}
}
