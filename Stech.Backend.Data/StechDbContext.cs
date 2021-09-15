using Microsoft.EntityFrameworkCore;
using Stech.Backend.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stech.Backend.Data
{
    public class StechDbContext:DbContext, IStechDbContext
    {
        public StechDbContext(DbContextOptions<StechDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        public new DbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public async Task SaveAsync()
        {
            await this.SaveChangesAsync();
        }
    }
}
