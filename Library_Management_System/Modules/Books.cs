using System.ComponentModel.DataAnnotations;

namespace Library_Management_System.Modules
{
    public class Books
    {
        [Key]
        public int BookId { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Genre { get; set; }
        public long ISBN { get; set; }
        public int PublicationYear { get; set; }
        public string? Status { get; set; }

    }
}
