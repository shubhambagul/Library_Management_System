using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_Management_System.Modules
{
    public class Transactions
    {
        [Key]
        public int TransactionID { get; set; }

        [ForeignKey("Users")]
        public int UserID { get; set; }

        [ForeignKey("Books")]
        public int BookID { get; set; }

        [Required]
        public DateTime BorrowDate { get; set; } = DateTime.Now;

        public DateTime? ReturnDate { get; set; }

        public decimal? FineAmount { get; set; }

        public Users User { get; set; }
        public Books Book { get; set; }
    }
}
