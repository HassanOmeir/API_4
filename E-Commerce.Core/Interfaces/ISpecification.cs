using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Interfaces
{
	// Will be implemented in repository layer .... 
	public interface ISpecification<T>
	{
        // What do Where take in LINQ ? --> Expression <Func<TEnity,bool>>
        // 1 - Where Criteria
        public Expression<Func<T,bool>> Criteria { get; }



		// What do Include take in LINQ ? --> Expression <Func<T,object>>
		// 2 - Include
		// We may have more than one Include !
		public List<Expression<Func<T, object>>> IncludeExpressions { get; }


		// What do OrderBy take in LINQ ? --> Expression <Func<T,object>>
		// 3 - OrderByAsc / OrderByDesc
		public Expression<Func<T, object>> OrderByAscExp { get; }
		public Expression<Func<T, object>> OrderByDescExp { get; }

        public int SkipSpec { get; }
        public int TakeSpec { get; }
        public bool IsPaginated { get; }
    }
}

// context.Set<>().Include().Include().Where().OrderBy
