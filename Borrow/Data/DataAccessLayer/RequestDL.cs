using Borrow.Models.Backend;
using Borrow.Models.Identity;

namespace Borrow.Data.DataAccessLayer
{
    public class RequestDL
    {
        private BorrowContext BorrowContext { get; set; }

        public RequestDL(BorrowContext borrowContext)
        {
            BorrowContext = borrowContext;  
        }

        public IEnumerable<BorrowRequest> OwnerGet(AppProfile profile)
        {
            return BorrowContext.BorrowRequests.Where(r => r.OwnerProfileId.Equals(profile.Id));
        }

        public IEnumerable<BorrowRequest> RequestGet(AppProfile profile)
        {
            return BorrowContext.BorrowRequests.Where(r => r.RequesterProfileId.Equals(profile.Id));
        }

        public void Create(BorrowRequest request)
        {
            BorrowContext.Add(request);
            BorrowContext.SaveChanges();
        }
    }
}
