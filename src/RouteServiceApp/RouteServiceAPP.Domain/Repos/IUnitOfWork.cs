using RouteServiceAPP.Domain.Dtos;
using RouteServiceAPP.Domain.Entities;
using RouteServiceAPP.Infrastructure.Repos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RouteServiceAPP.Domain.Repos
{
	public interface IUnitOfWork
	{
		IRouteRepo RouteRepo { get; }
		IBaseRepo<Locality> LocalityRepo { get; }
		IBaseRepo<RouteLocality> RouteLocalityRepo { get; }
		IBaseRepo<BookedRoute> BookedRouteRepo { get; }
		IBaseRepo<Seat> SeatRepo { get; }

		Task SaveAsync();

		Task BeginTransactionAsync();
		Task CommitTransactionAsync();
		Task RollbackTransactionAsync();
	}
}
