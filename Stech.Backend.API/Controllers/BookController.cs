using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public BookController(IBookRepository bookRepository)
        {
            this._bookRepository = bookRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = _bookRepository.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                //TODO: Logging
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult("Can't get books");
            }
        }
        [HttpGet("{bookId}")]
        public async Task<IActionResult> Get(int bookId)
        {
            try
            {
                var result = _bookRepository.Get(bookId);
                if (result is null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                //TODO: Logging
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
                //TODO: Logging
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult("Can't create a book");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Book book)
        {
            try
            {
                if(_bookRepository.Get(book.Id) is null)
                {
                    return BadRequest();
                }

                await _bookRepository.UpdateAsync(book);

                return Ok(book);
            }
            catch (Exception ex)
            {
                //TODO: Logging
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult("Can't update the book");
            }
        }

        [HttpDelete("{bookId}")]
        public async Task<IActionResult> Delete(int bookId)
        {
            try
            {
                var book = _bookRepository.Get(bookId);

                if(book is null)
                {
                    return BadRequest();
                }

                _bookRepository.Remove(book);
                return Ok();
            }
            catch (Exception ex)
            {
                //TODO: Logging
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult("Can't delete the book");

            }
        }

        [HttpPost("sell/{bookId}")]
        public async Task<IActionResult> SellBook(int bookId)
        {
            try
            {
                var book = _bookRepository.Get(bookId);
                if(book is null)
                {
                    return BadRequest();
                }
                await _bookRepository.Sell(bookId);

                return Ok();
            }
            catch (Exception)
            {
                //TODO: loggin
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult("Can't sell this book");
            }
        }
    }
}
