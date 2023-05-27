//using Ardalis.Specification;
//using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RouteServiceAPP.Domain.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RouteServiceAPP.Infrastructure.Repos
{
	public class BaseRepo<T> : IBaseRepo<T>
		where T : class
	{
		protected readonly RouteServiceContext _context;
		protected readonly DbSet<T> _table;
		public BaseRepo(RouteServiceContext context)
		{
			_context = context;
			_table = context.Set<T>();
		}

		public async Task<IEnumerable<T>> GetAsync(string[] includes = null)
		{
			IQueryable<T> dbRequest = _table;
			if(includes != null)
			{
				foreach (var include in includes)
				{
					dbRequest = dbRequest.Include(include);
				}
			}
			
			return await dbRequest.ToListAsync();
		}

		public async Task<T> GetAsync(object id)
		{
			return await _table.FindAsync(id);
		}

		//public async Task<T> GetSingleAsync(ISpecification<T> specification)
		//{
		//	return await ApplySpecification(specification).FirstOrDefaultAsync();
		//}
		//public async Task<IEnumerable<T>> GetAsync(ISpecification<T> specification)
		//{
		//	return await ApplySpecification(specification).ToListAsync();
		//}

		//public IQueryable<T> ApplySpecification(ISpecification<T> specification)
		//{
		//	return SpecificationEvaluator.Default.GetQuery(_table, specification);
		//}


		public async Task<T> CreateAsync(T entity)
		{
			var createdEntry = await _table.AddAsync(entity);
			return createdEntry.Entity;
		}

		public async Task UpdateAsync(T entity)
		{
			if (_context.Entry(entity).State == EntityState.Detached)
			{
				_table.Attach(entity);
			}

			_context.Entry(entity).State = EntityState.Modified;
		}

		public async Task DeleteAsync(object id)
		{
			T entityToRemove = await _context.FindAsync<T>(id);

			if (entityToRemove != null)
			{
				_table.Remove(entityToRemove);
			}
		}

		public async Task DeleteAsync(T entity)
		{
			if (_context.Entry(entity).State == EntityState.Detached)
			{
				_table.Attach(entity);
			}
			_table.Remove(entity);
		}


		public async Task<IEnumerable<T>> Where(Expression<Func<T, bool>> where)
		{
			return await _table.Where(where).ToListAsync();
		}


		public void LoadNavigationProperty(T entity, string propertyToLoad)
		{
			_context.Entry(entity).Navigation(propertyToLoad).Load();
		}
	}
}
