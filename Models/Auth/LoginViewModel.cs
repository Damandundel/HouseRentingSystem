using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace HouseRentingSystem.Models.Auth
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Invalid username")]
        public string Username { get; set; }
        [Required]
        [StringLength(80, MinimumLength = 6, ErrorMessage = "Invalid Password")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}