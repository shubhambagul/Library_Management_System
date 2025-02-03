using Library_Management_System.Modules;

namespace Library_Management_System.Service
{
    public interface IBookService
    {
        public bool AddNewBook(BooksDto bookDTO);
        public IEnumerable<Books> GetAllBooks(string? Title = null, string? Author = null, string? Genre = null, long? ISBN = null,string? SortBy = null, 
    string? Order = "asc");
        public Books getBooks(int id);
        public bool updateBook(int id, BooksDto bookDTO);
        public bool deleteBook(int id);
    }
}
