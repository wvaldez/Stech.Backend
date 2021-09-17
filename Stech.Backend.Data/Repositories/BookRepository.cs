using Microsoft.EntityFrameworkCore;
using Stech.Backend.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stech.Backend.Data
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private readonly IStechDbContext databaseContext;

        public BookRepository(IStechDbContext databaseContext) : base(databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task Sell(int bookId)
        {
            bool saveFailed = false;
            do
            {
                try
                {
                    saveFailed = false;
                    await Task.Delay(new Random().Next(100, 1000));
                    this.databaseContext.Books.First(b => b.Id == bookId).Sell();
                    await this.databaseContext.SaveAsync();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    // If there's a concurrency exception, try again in the next 1000 miliseconds
                    // it will give some time to other attempts be succesfull     
                    await Task.Delay(1000);
                    saveFailed = true;
                    e.Entries.Single().Reload();
                }                
            }
            while (saveFailed);


        }
    }
}