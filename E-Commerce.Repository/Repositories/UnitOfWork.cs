using E_Commerce.Core.Entities;
using E_Commerce.Core.Interfaces;
using E_Commerce.Core.Interfaces.Repositories;
using E_Commerce.Repository.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly DataContext _context;
		private readonly Hashtable RepoDictionary;

		public UnitOfWork(DataContext context)
		{
			_context = context;
			RepoDictionary = new Hashtable();
		}
		public async ValueTask DisposeAsync()
		{
			await _context.DisposeAsync();
		}

		public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
		{
			//return new GenericRepository<TEntity, TKey>(_context);        // bad way , will create new even if there is one there ! 
			                                                                // so we will use a HashTable (easier than a dictionary)

			var TypeName = typeof(TEntity).Name;
			if (RepoDictionary.ContainsKey(TypeName))
				return RepoDictionary[TypeName] as IGenericRepository<TEntity, TKey>;

			var repo = new GenericRepository<TEntity, TKey>(_context);
			RepoDictionary.Add(TypeName, repo);
			return repo;
		}

		public async Task<int> SaveAsync()
		{
			return await _context.SaveChangesAsync();
		}
	}
}
