using Stech.Backend.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Stech.Backend.Console
{
    class Program
    {
        private static HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            try
            {
                client.BaseAddress = new Uri("http://localhost:55623/");
                
                //First insert 5 books, for testing purpose
                AddBooks();
                
                // Get all books from list                
                var booksresponse = await client.GetAsync("api/book");
                var books = JsonSerializer.Deserialize<List<Book>>(await booksresponse.Content.ReadAsStringAsync());

                var tasksBooks = new List<Task>();

                System.Console.WriteLine("Book name\t|\tSales count\t|\tAverage elapsed time");

                Parallel.ForEach(books, (book) => 
                {
                    tasksBooks.Add(SellBook(book.Id));
                });
             
                Task.WaitAll(tasksBooks.ToArray());
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                throw;
            }
            
        }

        private static void AddBooks()
        {
            for(int i = 1; i <= 5; i++)
            {
                Book book = new Book();
                book.Name = "Book " + i;
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, "api/book/");
                requestMessage.Content = new StringContent(JsonSerializer.Serialize(book), Encoding.UTF8, "application/json");
                client.SendAsync(requestMessage);
            }            
        }

        public static async Task SellBook(int bookId)
        {
            var tasks = new List<Task<HttpResponseMessage>>();
            Stopwatch sw = Stopwatch.StartNew();

            for (int i = 0; i < 1000; i++)
            {
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, "api/book/sell/" + bookId);
                tasks.Add(client.SendAsync(requestMessage));
            }

            Task.WaitAll(tasks.ToArray());
            sw.Stop();

            // Retrieve up to date book information
            var bookresponse = await client.GetAsync("api/book/"+bookId);
            var book = JsonSerializer.Deserialize<Book>(await bookresponse.Content.ReadAsStringAsync());
            
            // Print to screen
            System.Console.WriteLine(book.Name + "\t|\t" + book.SalesCount + "\t|\t" + sw.ElapsedMilliseconds / 1000 + "ms");
        }
    }
}
