using Library_Management_System.Exceptions;
using Library_Management_System.Modules;
using Library_Management_System.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactions _context;
        private readonly Transactions _transactions;
        public TransactionController(ITransactions context, Transactions transactions)
        {
            _context = context;
            _transactions = transactions;
        }
        [HttpPost]
        [Route("BorrowBook")]
        [Authorize(Roles = "Librarian,Member")]
        public async Task<IActionResult> borrowBook(int userId, int bookId)
        {
            try
            {
                var result = await _context.borrowBook(userId, bookId);
                
                return Ok(new { Message = result });
            }
            catch (Exception ex) {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("ReturnBook")]
        [Authorize(Roles = "Librarian,Member")]
        public async Task<IActionResult> ReturnBook(int userId, int bookId)
        {
            try
            {
                var result = await _context.returnBook(userId, bookId);
                return Ok(new { Message = result });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An error occurred while processing your request.", Details = ex.Message });
            }
        }
    }
}
