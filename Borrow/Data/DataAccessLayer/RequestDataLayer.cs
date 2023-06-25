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
            return BorrowContext.BorrowRequests.Where(r => r.OwnerId.Equals(profile.OwnerId));
        }

        public IEnumerable<BorrowRequest> Outgoing(AppProfile profile)
        {
            return BorrowContext.BorrowRequests.Where(r => r.RequesterOwnerId.Equals(profile.OwnerId));
        }

        public void Create(BorrowRequest request)
        {
            BorrowContext.Add(request);
            BorrowContext.SaveChanges();
        }
    }
}
