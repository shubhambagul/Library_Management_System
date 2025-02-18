using Library_Management_System.Modules;
using Library_Management_System.Repositories;
using Library_Management_System.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController(IBookService _context, Books _book) : ControllerBase
    {
        //private readonly IBookService _context;
        //private readonly Books _book;

        //public BookController(IBookService context, Books book)
        //{
        //    _context = context;
        //    _book = book;
        //}

        [HttpGet]
        [Route("GetAllBook")]
        [Authorize(Roles = "Admin")]
        public ActionResult GetAllBooks([FromQuery] string? Title, [FromQuery] string? Author, [FromQuery] string? Genre, [FromQuery] long? ISBN, [FromQuery] string? SortBy,
    [FromQuery] string? Order)
        {
            try
            {
                var getAllBooks = _context.GetAllBooks(Title, Author, Genre, ISBN, SortBy, Order);
                if (!getAllBooks.Any())
                {
                    return NotFound("Books are not available");
                }
                return Ok(getAllBooks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("GetBook")]
      //  [Authorize(Roles = "Admin,Member")]
        public ActionResult GetBook(int id)
        {
            try
            {
                var getBook = _context.getBooks(id);
               
                return Ok(getBook);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("AddBook")]
        [Authorize(Roles = "Admin")]
        public ActionResult AddNewBook([FromBody] BooksDto bookDTO)
        {
            try
            {
                var addBook = _context.AddNewBook(bookDTO);
                return Ok("New book added successfully");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        [HttpPut]
        [Route("UpdateBook")]
        [Authorize(Roles = "Admin")]
        public ActionResult updateBook(int id, BooksDto bookDTO)
        {
            try
            {
                var updateResult = _context.updateBook(id, bookDTO);
                if (!updateResult)
                {
                    return NotFound($"No book found with ID {id}.");
                }
                return Ok("Book updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("DeleteBook")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteBook(int id)
        {
            try
            {
                var deleteResult = _context.deleteBook(id);
                if (!deleteResult)
                {
                    return NotFound($"No book found with ID {id}.");
                }
                return Ok("Book deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
