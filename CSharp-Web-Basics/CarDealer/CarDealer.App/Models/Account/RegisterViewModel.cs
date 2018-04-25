using System.ComponentModel.DataAnnotations;

namespace CarDealer.App.Models.Account
{
    public class RegisterViewModel
    {
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }
    }
}
