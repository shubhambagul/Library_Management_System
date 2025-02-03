using Library_Management_System.Exceptions;
using Library_Management_System.Modules;
using Library_Management_System.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Globalization;

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
        public IEnumerable<Books> GetAllBooks(string? Title = null, string? Author = null, string? Genre = null, long? ISBN = null, string? SortBy = null,
    string? Order = "asc")
        {
            try
            {
                var query = _context.books.AsQueryable();

                if (!string.IsNullOrEmpty(Title))
                {
                    query = query.Where(x => x.Title.Contains(Title));
                }
                if (!string.IsNullOrEmpty(Author))
                {
                    query = query.Where(x => x.Author.Contains(Author));
                }
                if (!string.IsNullOrEmpty(Genre))
                {
                    query = query.Where(x => x.Genre.Contains(Genre));
                }
                if (ISBN.HasValue)  
                {
                    query = query.Where(x => x.ISBN == ISBN.Value);
                }
                if (!string.IsNullOrEmpty(SortBy))
                {
                    switch (SortBy.ToLower())
                    {
                        case "title":
                            query = (Order.ToLower() == "desc") ? query.OrderByDescending(x => x.Title) : query.OrderBy(x => x.Title);
                            break;
                        case "author":
                            query = (Order.ToLower() == "desc") ? query.OrderByDescending(x => x.Author) : query.OrderBy(x => x.Author);
                            break;
                        case "genre":
                            query = (Order.ToLower() == "desc") ? query.OrderByDescending(x => x.Genre) : query.OrderBy(x => x.Genre);
                            break;
                        case "isbn":
                            query = (Order.ToLower() == "desc") ? query.OrderByDescending(x => x.ISBN) : query.OrderBy(x => x.ISBN);
                            break;
                        case "publicationyear":
                            query = (Order.ToLower() == "desc") ? query.OrderByDescending(x => x.PublicationYear) : query.OrderBy(x => x.PublicationYear);
                            break;
                        default:
                          
                            break;
                    }
                }

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Books getBooks(int id)
        {
            try
            {
                var getBook = _context.books.FirstOrDefault(x => x.BookId == id);

                if (getBook == null)
                {
                    throw new NotFoundException("Book not found");
                }
                return getBook;
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
        public bool AddNewBook(BooksDto bookDTO)
        {
            try
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
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }

        }
        public bool updateBook(int id, BooksDto bookDTO)
        {
            try
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
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
        public bool deleteBook(int id)
        {
            try
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
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
    }
}
