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

        public BorrowRequest? Get(int id)
        {
            return BorrowContext.BorrowRequests.Where(r => r.Id.Equals(id)).FirstOrDefault();
        }

        public IEnumerable<BorrowRequest> Get(IEnumerable<int> ids)
        {
            return BorrowContext.BorrowRequests.Where(r => ids.Contains(r.Id));
        }

        internal void Update(BorrowRequest newRequest)
        {
            var request = BorrowContext.BorrowRequests.Where(r => r.Id.Equals(newRequest.Id)).FirstOrDefault();
            request = newRequest;
            BorrowContext.SaveChanges();
        }

        internal void Update(IEnumerable<BorrowRequest> newRequest)
        {
            foreach(var request in newRequest)
            {
                var currentRequest = BorrowContext.BorrowRequests.Where(r => r.Id.Equals(request.Id)).FirstOrDefault();
                currentRequest = request;
                BorrowContext.SaveChanges();
            }
        }
    }
}
