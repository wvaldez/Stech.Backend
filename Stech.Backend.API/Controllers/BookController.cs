using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stech.Backend.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stech.Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<BookController> _logger;


        public BookController(IBookRepository bookRepository, ILogger<BookController> logger)
        {
            _bookRepository = bookRepository;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _bookRepository.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can't get books");
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult("Can't get books");
            }
        }
        [HttpGet("{bookId}")]
        public async Task<IActionResult> Get(int bookId)
        {
            try
            {
                var result = await _bookRepository.Get(bookId);
                if (result is null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can't get book");
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult("Can't get the book");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Book book)
        {
            try
            {
                var result = await _bookRepository.AddAsync(book);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can't create book");
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult("Can't create a book");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Book book)
        {
            try
            {
                var entity = await _bookRepository.Get(book.Id);
                if (entity is null)
                {
                    return BadRequest();
                }

                entity = book;

                await _bookRepository.SaveChanges();

                return Ok(book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can't update book");
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult("Can't update the book");
            }
        }

        [HttpDelete("{bookId}")]
        public async Task<IActionResult> Delete(int bookId)
        {
            try
            {
                var book = await _bookRepository.Get(bookId);

                if (book is null)
                {
                    return BadRequest();
                }

                await _bookRepository.Remove(book);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can't delete book");
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult("Can't delete the book");

            }
        }

        [HttpPost("sell/{bookId}")]
        public async Task<IActionResult> SellBook(int bookId)
        {
            try
            {
                var book = await _bookRepository.Get(bookId);
                if (book is null)
                {
                    return BadRequest();
                }

                await _bookRepository.Sell(book.Id);


                _logger.LogInformation("SellBook called " + book.Name + ". Sale #" + book.SalesCount);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can't sell book");
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult("Can't sell this book");
            }
        }
    }
}
