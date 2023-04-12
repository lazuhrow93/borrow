using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Borrow.Models.Identity;

namespace Borrow.Data
{
    public class BorrowContext : IdentityDbContext<User>
    {
        public BorrowContext (DbContextOptions<BorrowContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    UserName = "lazuhrow93",
                    PasswordHash = "1234567899",
                    Email = "test@tset.com",
                    FirstName = "Lazaro", 
                    LastName = "Hernandez",
                    PhoneNumber = "2813308004",
                }
            );
        }

        #region User

        public User GetUser(User user)
        {
            return (this.User?.Where(u => u.UserName == user.UserName)).First();
        }

        public bool UserNameExists(User user)
        {
            return (this.User?.Any(e => e.UserName == user.UserName)).GetValueOrDefault();
        }

        #endregion
    }
}
