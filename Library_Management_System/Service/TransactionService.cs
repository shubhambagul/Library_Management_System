using Library_Management_System.Exceptions;
using Library_Management_System.Modules;
using Library_Management_System.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Service
{
    public class TransactionService : ITransactions
    {
        private readonly LibraryManagementContext _context;
        private readonly Transactions _transactions;
        public TransactionService(LibraryManagementContext context, Transactions transactions)
        {
            _context = context;
            _transactions = transactions;
        }

        public async Task<string> borrowBook(int userId, int bookId)
        {
            try
            {
                var user = await _context.users.FindAsync(userId);
                var book = await _context.books.FindAsync(bookId);

                if (user == null || book == null)
                {
                    return "User or Book not found.";
                }
                //if(user.Role == "Admin")
                //{
                //    throw new NotFoundException($"Admin should not checkout book.");
                //}
                if (book.Status == "Checked Out")
                {
                    return "The book is already borrowed.";
                }
                if (book.Status == "Reserved")
                {
                    return "Book is reserved.. You can not borrow the book";
                }
                var transaction = new Transactions
                {
                    UserID = userId,
                    BookID = bookId,
                    BorrowDate = DateTime.Now,
                    ReturnDate = null,
                    FineAmount = null,
                };
                book.Status = "Checked Out";
                _context.transactions.Add(transaction);
                _context.books.Update(book);
                await _context.SaveChangesAsync();

                return "Book borrowed successfully!";
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> returnBook(int userId, int bookId)
        {
            try
            {
                var transaction = await _context.transactions
                    .FirstOrDefaultAsync(t => t.UserID == userId && t.BookID == bookId && t.ReturnDate == null);

                if (transaction == null)
                {
                    throw new NotFoundException("No active transaction found for the given user and book.");
                }

                var book = await _context.books.FindAsync(bookId);
                if (book == null)
                {
                    throw new NotFoundException("Book not found.");
                }

                DateTime currentDate = DateTime.Now;
                transaction.ReturnDate = currentDate;

                int allowedDays = 5;
                decimal finePerDay = 10;

                var overdueDays = (currentDate - transaction.BorrowDate).Days - allowedDays;
                if (overdueDays > 0)
                {
                    transaction.FineAmount = overdueDays * finePerDay;
                }
                else
                {
                    transaction.FineAmount = 0;
                }

                book.Status = "Available";

                _context.transactions.Update(transaction);
                _context.books.Update(book);
                await _context.SaveChangesAsync();

                return transaction.FineAmount > 0
                    ? $"Book returned successfully! Overdue fine: INR {transaction.FineAmount}"
                    : "Book returned successfully! No fine incurred.";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

