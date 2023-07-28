using Borrow.Models.Listings;

namespace Borrow.Models.Views
{
    public class NewItemViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateAcquired { get; set; }
        public string? UserName { get; set; }

        public Item Parse()
        {
            return new Item()
            {
                Name = Name,
                Description = Description,
                OwnedSince = DateAcquired,
                UserName = UserName
            };
        }
    }
}
