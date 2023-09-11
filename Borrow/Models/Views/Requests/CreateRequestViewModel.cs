using Borrow.Models.Backend;
using Borrow.Models.Views.TableViews;
using static Borrow.Models.Backend.Request;

namespace Borrow.Models.Views.Requests
{
    public class CreateRequestViewModel
    {
        public ListingViewModel ListingViewModel { get; set; }
        public DateTime ReturnDateUtc { get; set; } = DateTime.UtcNow;
        public RequestType RequestType { get; set; }
        public ItemViewModel ItemInformation { get; set; }
        public decimal RequestRate { get; set; }

        public CreateRequestViewModel()
        {

        }

        public CreateRequestViewModel(Item item)
        {
            ItemInformation = new(item);
        }
    }
}
