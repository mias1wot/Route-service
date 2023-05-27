using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RouteServiceAPP.Domain.Repos
{
	public interface IBaseRepo<T>
		where T: class
	{
		Task<IEnumerable<T>> GetAsync(string[] includes = null);
		Task<T> GetAsync(object id);
		//Task<T> GetSingleAsync(ISpecification<T> specification);
		//Task<IEnumerable<T>> GetAsync(ISpecification<T> specification);

		Task<T> CreateAsync(T entity);

		Task UpdateAsync(T entity);

		Task DeleteAsync(object id);
		Task DeleteAsync(T entity);

		Task<IEnumerable<T>> Where(Expression<Func<T, bool>> where);

		void LoadNavigationProperty(T entity, string propertyToLoad);
	}
}
