using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Borrow.Models.Views;
using Borrow.Models.Backend;

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
        public DbSet<Listing> Listing { get; set; }
        public DbSet<Neighborhood> Neighborhood { get; set; }
        public DbSet<AppProfile> AppProfile { get; set; }
        public DbSet<ListValue> BorrowEnumeration { get; set; }
        public DbSet<ListValue> ListValue { get; set; }
        public DbSet<Request> Request { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppProfile>().HasData(
                new AppProfile()
                {
                    Id = 1,
                    OwnerId = 1,
                    NeighborhoodId = 1,
                    UserName = "dummy93"
                });

            modelBuilder.Entity<Neighborhood>().HasData(
                new Neighborhood()
                {
                    Id = 1,
                    Name = "Spring Lakes"
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
