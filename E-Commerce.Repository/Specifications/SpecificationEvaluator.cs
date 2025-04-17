using E_Commerce.Core.Entities;
using E_Commerce.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repository.Specifications
{
	public class SpecificationEvaluator<TEntity, TKey> where TEntity : BaseEntity<TKey>
	{
		public static IQueryable<TEntity> BuildQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification)
		{

			// ------------ Where (criteria)
			var query = inputQuery;
			if (specification.Criteria is not null)
				query = query.Where(specification.Criteria);


			// ------------ Includes
			if (specification.IncludeExpressions.Any())
			{
				foreach (var item in specification.IncludeExpressions)
				{
					query = query.Include(item);
				}
				// or
				// query = specification.IncludeExpressions.Aggregate(query, (current, exp) => current.Include(exp));
			}

			// ------------ OrderBy
			if (specification.OrderByAscExp is not null)
			{
				query = query.OrderBy(specification.OrderByAscExp);
			}
			if (specification.OrderByDescExp is not null)
			{
				query = query.OrderByDescending(specification.OrderByDescExp);

			}



			if (specification.IsPaginated)
			{
				query = query.Skip(specification.SkipSpec);
				query = query.Take(specification.TakeSpec);
			}

			return query;

		}
	}
}
