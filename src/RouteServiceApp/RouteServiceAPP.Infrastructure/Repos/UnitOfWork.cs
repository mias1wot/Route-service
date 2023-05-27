using RouteServiceAPP.Domain.Dtos;
using RouteServiceAPP.Domain.Entities;
using RouteServiceAPP.Domain.Repos;
using RouteServiceAPP.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

// For fast-replace
// From: _.+?Repo
namespace RouteServiceAPP.Infrastructure.Repos
{
	public class UnitOfWork : IDisposable, IUnitOfWork
	{
		RouteServiceContext _context;
		IRouteRepo _routeRepo;
		IBaseRepo<Locality> _localityRepo;
		IBaseRepo<RouteLocality> _routeLocalityRepo;
		IBaseRepo<BookedRoute> _bookedRouteRepo;
		IBaseRepo<Seat> _seatRepo;

		public UnitOfWork(RouteServiceContext context)
		{
			_context = context;
		}

		public IRouteRepo RouteRepo
		{
			get
			{
				if (_routeRepo is null)
				{
					_routeRepo = new RouteRepo(_context);
				}
				return _routeRepo;
			}
		}

		public IBaseRepo<Locality> LocalityRepo
		{
			get
			{
				if (_localityRepo is null)
				{
					_localityRepo = new BaseRepo<Locality>(_context);
				}
				return _localityRepo;
			}
		}

		public IBaseRepo<RouteLocality> RouteLocalityRepo
		{
			get
			{
				if (_routeLocalityRepo is null)
				{
					_routeLocalityRepo = new BaseRepo<RouteLocality>(_context);
				}
				return _routeLocalityRepo;
			}
		}

		public IBaseRepo<BookedRoute> BookedRouteRepo
		{
			get
			{
				if (_bookedRouteRepo is null)
				{
					_bookedRouteRepo = new BaseRepo<BookedRoute>(_context);
				}
				return _bookedRouteRepo;
			}
		}
		public IBaseRepo<Seat> SeatRepo
		{
			get
			{
				if (_seatRepo is null)
				{
					_seatRepo = new BaseRepo<Seat>(_context);
				}
				return _seatRepo;
			}
		}


		public async Task SaveAsync()
		{
			await _context.SaveChangesAsync();
		}


		public async Task BeginTransactionAsync()
		{
			await _context.Database.BeginTransactionAsync();
		}

		public async Task CommitTransactionAsync()
		{
			await _context.Database.CommitTransactionAsync();
		}

		public async Task RollbackTransactionAsync()
		{
			await _context.Database.RollbackTransactionAsync();
		}


		private bool disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					_context.Dispose();
				}
			}
			disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
