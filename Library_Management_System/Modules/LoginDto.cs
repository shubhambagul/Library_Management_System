using System.ComponentModel.DataAnnotations;

namespace Library_Management_System.Modules
{
    public class LoginDto
    {
        public string Email { get; set; }
        [Required(ErrorMessage ="required.....!")]
        public string Password { get; set; }
    }
}
