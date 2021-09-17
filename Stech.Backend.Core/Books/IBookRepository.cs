using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stech.Backend.Core
{
    public interface IBookRepository:IRepository<Book>
    {
        public Task Sell(int bookId);
    }
}
