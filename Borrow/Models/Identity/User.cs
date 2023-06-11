using Microsoft.AspNetCore.Identity;

namespace Borrow.Models.Identity
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ProfileId { get; set; }
    }
}
