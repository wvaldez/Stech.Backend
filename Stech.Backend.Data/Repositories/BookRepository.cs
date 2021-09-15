using Stech.Backend.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stech.Backend.Data
{
    public class BookRepository:Repository<Book>, IBookRepository
    {
        private readonly IStechDbContext databaseContext;

        public BookRepository(IStechDbContext databaseContext):base(databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task Sell(int bookId)
        {
            this.databaseContext.Books.First(x => x.Id == bookId).SalesCount++;
            await this.databaseContext.SaveAsync();
        }
    }
}
