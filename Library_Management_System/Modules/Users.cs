using System.ComponentModel.DataAnnotations;

namespace Library_Management_System.Modules
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public DateTime MembershipDate { get; set; }=DateTime.Now;

    }
}
