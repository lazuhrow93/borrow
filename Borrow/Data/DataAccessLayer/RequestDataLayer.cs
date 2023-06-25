using Borrow.Models.Backend;
using Borrow.Models.Identity;

namespace Borrow.Data.DataAccessLayer
{
    public class RequestDataLayer
    {
        private BorrowContext BorrowContext { get; set; }

        public RequestDataLayer(BorrowContext borrowContext)
        {
            BorrowContext = borrowContext;  
        }

        public IEnumerable<BorrowRequest> Incoming(AppProfile profile)
        {
            return BorrowContext.BorrowRequests.Where(r => r.OwnerKey.Equals(profile.RequestKey));
        }

        public IEnumerable<BorrowRequest> Outgoing(AppProfile profile)
        {
            return BorrowContext.BorrowRequests.Where(r => r.RequesterKey.Equals(profile.RequestKey));
        }

        public void Create(BorrowRequest request)
        {
            BorrowContext.Add(request);
            BorrowContext.SaveChanges();
        }
    }
}
