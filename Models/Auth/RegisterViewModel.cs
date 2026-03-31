using System.ComponentModel.DataAnnotations;

namespace HouseRentingSystem.Models.Auth
{ 
   public class RegisterViewModel
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        [Display(Name = "Username")]
        public string Username { get; set; } = null!;

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6)]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;

    }
}