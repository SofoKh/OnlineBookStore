using System.ComponentModel.DataAnnotations;

namespace _3maisiproeqti.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public int Role { get; set; }
    }
}
