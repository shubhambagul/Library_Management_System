using Library_Management_System.Modules;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Repositories
{
    public class LibraryManagementContext : DbContext
    {
        public LibraryManagementContext(DbContextOptions<LibraryManagementContext> options) : base(options)
        {

        }
        public DbSet<Users> users { get; set; }
        public DbSet<Books> Books { get; set; }
    }
}
