using System.ComponentModel.DataAnnotations;

namespace Borrow.Models.Identity.Views
{
    public class RegisterViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter Username")]
        [StringLength(255)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
