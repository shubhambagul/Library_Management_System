namespace Library_Management_System.Modules
{
    public class BooksDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public long ISBN { get; set; }
        public int PublicationYear { get; set; }
        public string Status { get; set; }

       // public ICollection<Transactions> Transactions { get; set; }
    }
}
