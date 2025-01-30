using Library_Management_System.Modules;

namespace Library_Management_System.Service
{
    public interface ITransactions
    {
        Task<string> borrowBook(int userId, int bookId);

        Task<string> returnBook(int userId, int bookId);
    }
}
