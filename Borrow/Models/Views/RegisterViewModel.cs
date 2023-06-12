using System.ComponentModel.DataAnnotations;

namespace Borrow.Models.Views
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter FirstName")]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter LastName")]
        [StringLength(255)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(255)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter Email")]
        [StringLength(255)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter Username")]
        [StringLength(255)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please select your Neighborhood")]
        [Display(Name = "Neighborhood Id")]
        public int Neighborhood { get; set; }
    }
}
