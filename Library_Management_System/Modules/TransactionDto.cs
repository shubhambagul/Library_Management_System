using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library_Management_System.Modules
{
    public class TransactionDto
    {
        public int UserID { get; set; }

        public int BookID { get; set; }

        public DateTime? ReturnDate { get; set; }

        public decimal? FineAmount { get; set; }

    
    }
}
