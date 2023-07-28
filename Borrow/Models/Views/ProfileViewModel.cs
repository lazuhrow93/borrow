using Borrow.Models.Identity;
using Borrow.Models.Listings;
using Borrow.Models.Views.TableViews;
using Microsoft.EntityFrameworkCore;
using System;

namespace Borrow.Models.Views
{
    public class ProfileViewModel
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Exchanges { get; set; }
        public List<ItemViewModel> OwnerItems { get; set; }
        public int? RemoveAtIndex { get; set; }
        public Guid EditItem { get; set; }

        public ProfileViewModel(User user, IEnumerable<Item> items)
        {
            Username = user.UserName;
            FirstName = user.FirstName;
            LastName = user.LastName;

            OwnerItems = new();
            foreach(var item in items)
            {
                OwnerItems.Add(new ItemViewModel(item));
            }
        }

        public void RemoveFromProfile(int index)
        {
            this.OwnerItems.RemoveAt(index);
        }
    }
}
