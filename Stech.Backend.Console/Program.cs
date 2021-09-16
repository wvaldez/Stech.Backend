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
            // Get all books from list
            client.BaseAddress = new Uri("http://localhost:55623/");
            var booksresponse = await client.GetAsync("api/book");
            var books = JsonSerializer.Deserialize<List<Book>>(await booksresponse.Content.ReadAsStringAsync());
            System.Console.WriteLine("Book name | Sales count | Average elapsed time");

            foreach (var book in books.Take(5))
            {
                Task.Run(SellBook(book.Id));
            }
        }

        public static async Action SellBook(int bookId)
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
            client.BaseAddress = new Uri("http://localhost:55623/");
            var bookresponse = await client.GetAsync("api/book/"+bookId);
            var book = JsonSerializer.Deserialize<Book>(await bookresponse.Content.ReadAsStringAsync());
            
            // Print to screen
            System.Console.WriteLine(book.Name + " | " + book.SalesCount + " | " + sw.ElapsedMilliseconds / 1000 + "ms");
        }
    }
}
