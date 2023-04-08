using System.ComponentModel.DataAnnotations;

namespace Borrow.Models.Identity.Views
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please Enter User Name")]
        [StringLength(255)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set;}
    }
}
