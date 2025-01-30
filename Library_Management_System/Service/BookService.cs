using Library_Management_System.Exceptions;
using Library_Management_System.Modules;
using Library_Management_System.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Library_Management_System.Service
{

    public class BookService : IBookService
    {
        private readonly LibraryManagementContext _context;
        private readonly Books _book;

        public BookService(LibraryManagementContext context, Books book)
        {
            _context = context;
            _book = book;
        }
        public IEnumerable<Books> GetAllBooks()
        {
            try
            {
                return _context.books.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Books getBooks(int id)
        {
            var getBook = _context.books.FirstOrDefault(x => x.BookId == id);

            if (getBook == null)
            {
                throw new NotFoundException("Book not found");
            }
            return getBook;
        }
        public bool AddNewBook(BooksDto bookDTO)
        {

            _book.Title = bookDTO.Title;
            _book.Author = bookDTO.Author;
            _book.Genre = bookDTO.Genre;
            _book.ISBN = bookDTO.ISBN;
            _book.PublicationYear = bookDTO.PublicationYear;
            _book.Status = bookDTO.Status;
            _context.books.Add(_book);
            _context.SaveChanges();
            return true;

        }
        public bool updateBook(int id, BooksDto bookDTO)
        {
            var result = _context.books.FirstOrDefault(x => x.BookId == id);
            if (result == null)
            {
                return false;
            }

            result.Title = bookDTO.Title;
            result.Author = bookDTO.Author;
            result.Genre = bookDTO.Genre;
            result.ISBN = bookDTO.ISBN;
            result.PublicationYear = bookDTO.PublicationYear;
            result.Status = bookDTO.Status;
            _context.books.Update(result);
            _context.SaveChanges();
            return true;
        }
        public bool deleteBook(int id)
        {
            var result = _context.books.FirstOrDefault(x => x.BookId == id);

            if (result == null)
            {
                return false;
            }
            _context.Remove(result);
            _context.SaveChanges();
            return true;
        }
    }
}
