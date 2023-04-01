using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Borrow.Models;

namespace Borrow.Data
{
    public class BorrowContext : DbContext
    {
        public BorrowContext (DbContextOptions<BorrowContext> options)
            : base(options)
        {
        }

        public DbSet<Borrow.Models.User> User { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = 1,
                    UserName = "lazuhrow93",
                    PasswordHash = "1234567899",
                    EmailAddress = "test@tset.com",
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
