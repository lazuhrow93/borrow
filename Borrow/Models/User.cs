using System.ComponentModel.DataAnnotations;

namespace Borrow.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public int FirstName { get; set; }
        public int LastName { get; set; }
        public int UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string PasswordHash { get; set; }

    }
}
