using Stech.Backend.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stech.Backend.Data
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly IStechDbContext _database;

        public Repository(IStechDbContext database)
        {
            _database = database;
        }

        public async Task<IQueryable<T>> GetAll()
        {
            return _database.Set<T>();
        }

        public async Task<T> Get(int id)
        {
            return _database.Set<T>()                
                .FirstOrDefault(p => p.Id == id);
        }

        public async Task<T> AddAsync(T entity)
        {
            var result = await _database.Set<T>().AddAsync(entity);
            await _database.SaveAsync();
            return result.Entity;
        }

        public async Task Remove(T entity)
        {
            _database.Set<T>().Remove(entity);
            await _database.SaveAsync();
        }

        public async Task SaveChanges()
        {
            await _database.SaveAsync();
        }
    }
}
