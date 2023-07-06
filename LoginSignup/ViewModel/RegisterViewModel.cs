using System.ComponentModel.DataAnnotations;

namespace LoginSignup.ViewModel
{
    public class RegisterViewModel
    {
         
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
