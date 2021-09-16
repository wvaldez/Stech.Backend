using System.Linq;
using System.Threading.Tasks;

namespace Stech.Backend.Core
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();

        T Get(int id);

        Task<T> AddAsync(T entity);

        void Remove(T entity);

        void SaveChanges();
    }
}
