using E_Commerce.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Specifications
{
	public class BaseSpecifications<T> : ISpecification<T>
	{
		// For initializing 
		public Expression<Func<T, bool>> Criteria { get; }
		public BaseSpecifications(Expression<Func<T, bool>> criteria)
		{
			Criteria = criteria;
		}

		public List<Expression<Func<T, object>>> IncludeExpressions { get; } = new();

		public Expression<Func<T, object>> OrderByAscExp { get; protected set; }

		public Expression<Func<T, object>> OrderByDescExp { get; protected set; }
		public int SkipSpec { get; protected set; }
		public int TakeSpec { get; protected set; }
		public bool IsPaginated { get; protected set; }

		protected void ApplyPagination(int PageSize , int PageIndex)
		{
			IsPaginated = true;
			SkipSpec = (PageIndex - 1) * PageSize;
			TakeSpec = PageSize;
		}
	}
}
