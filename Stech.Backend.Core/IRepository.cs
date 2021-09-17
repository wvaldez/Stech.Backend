using System.Linq;
using System.Threading.Tasks;

namespace Stech.Backend.Core
{
    public interface IRepository<T>
    {
        Task<IQueryable<T>> GetAll();

        Task<T> Get(int id);

        Task<T> AddAsync(T entity);

        Task Remove(T entity);

        Task SaveChanges();
    }
}
