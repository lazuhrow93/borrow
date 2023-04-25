﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Borrow.Models.Identity;
using Borrow.Models.Views;
using Borrow.Models.Listings;

namespace Borrow.Data
{
    public class BorrowContext : IdentityDbContext<User>
    {
        public BorrowContext (DbContextOptions<BorrowContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; } = default!;
        public DbSet<Item>? Item { get; set; }

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
                    OwnerId = 2
                }
            );
            modelBuilder.Entity<Item>().HasData(
                new Item
                {
                    Id = 1,
                    Name = "Lawn Mower",
                    Description = $"Machine to mow lawns",
                    Age = new TimeSpan(1, 0, 0, 0),
                    DailyRate = 10.00M,
                    OwnerId = 1
                },
                new Item
                {
                    Id = 2,
                    Name = "Weed Eater",
                    Description = $"Machine to Trim and cut lawns",
                    Age = new TimeSpan(1, 0, 0, 0),
                    DailyRate = 5.00M,
                    OwnerId = 1
                },
                new Item
                {
                    Id = 3,
                    Name = "Leaf Blower",
                    Description = $"Machine to blow",
                    Age = new TimeSpan(1, 0, 0, 0),
                    DailyRate = 5.00M,
                    OwnerId = 1
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
