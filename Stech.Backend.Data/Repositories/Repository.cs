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

        public IQueryable<T> GetAll()
        {
            return _database.Set<T>();
        }

        public T Get(int id)
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

        public void Remove(T entity)
        {
            _database.Set<T>().Remove(entity);
            _database.SaveAsync();
        }

        public Task<T> UpdateAsync(T entity)
        {
            _database.SaveAsync();
            throw new NotImplementedException();   
        }
    }
}
