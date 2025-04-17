using E_Commerce.Core.Entities;
using E_Commerce.Core.Interfaces;
using E_Commerce.Core.Interfaces.Repositories;
using E_Commerce.Repository.Data;
using E_Commerce.Repository.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Repositories
{
	public class GenericRepository <TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
	{
		private readonly DataContext _context;
		public GenericRepository(DataContext context)
        {
			_context = context;
		}

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _context.Set<TEntity>().ToListAsync();

		public async Task<TEntity> GetAsync(TKey id) => await _context.Set<TEntity>().FindAsync(id);
		
		public async Task AddAsync(TEntity entity) => await _context.Set<TEntity>().AddAsync(entity);

		public void Update(TEntity entity) => _context.Set<TEntity>().Update(entity);
		
		public void Delete(TEntity entity) => _context.Set<TEntity>().Remove(entity);



		public async Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecification<TEntity> specification)
		{
			// return await SpecificationEvaluator<TEntity,TKey>.BuildQuery(_context.Set<TEntity>() , specification).ToListAsync();
			
			return await ApplySpecification(specification).ToListAsync();
		}

		public async Task<TEntity> GetWithSpecAsync(ISpecification<TEntity> specification)
		{
			//return await SpecificationEvaluator<TEntity, TKey>.BuildQuery(_context.Set<TEntity>(), specification).FirstOrDefaultAsync();

			return await ApplySpecification(specification).FirstOrDefaultAsync();
		}
		public async Task<int> GetCountWithSpecAsync(ISpecification<TEntity> specification)
		{
			return await ApplySpecification(specification).CountAsync();
		}




		// for more readable code , make this code in a function and use it multiple times : 
		private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
		{
			return SpecificationEvaluator<TEntity, TKey>.BuildQuery(_context.Set<TEntity>(), specification);
		}

	}
}
