
using Microsoft.EntityFrameworkCore;
using Stech.Backend.Core;
using System.Threading.Tasks;

namespace Stech.Backend.Data
{
    public interface IStechDbContext
    {
        DbSet<Book> Books { get; set; }
        DbSet<Author> Authors { get; set; }

        DbSet<T> Set<T>() where T : class;

        Task SaveAsync();
    }
}
