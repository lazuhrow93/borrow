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

        public IEnumerable<Request> Incoming(AppProfile profile)
        {
            return BorrowContext.Request.Where(r => r.LenderKey.Equals(profile.RequestKey));
        }

        public IEnumerable<Request> Outgoing(AppProfile profile)
        {
            return BorrowContext.Request.Where(r => r.RequesterKey.Equals(profile.RequestKey));
        }

        public void Create(Request request)
        {
            BorrowContext.Add(request);
            BorrowContext.SaveChanges();
        }

        public Request? Get(int id)
        {
            return BorrowContext.Request.Where(r => r.Id.Equals(id)).FirstOrDefault();
        }

        public IEnumerable<Request> Get(IEnumerable<int> ids)
        {
            return BorrowContext.Request.Where(r => ids.Contains(r.Id));
        }

        internal void Update(Request newRequest)
        {
            var request = BorrowContext.Request.Where(r => r.Id.Equals(newRequest.Id)).FirstOrDefault();
            request = newRequest;
            BorrowContext.SaveChanges();
        }

        internal void Update(IEnumerable<Request> newRequest)
        {
            foreach(var request in newRequest)
            {
                var currentRequest = BorrowContext.Request.Where(r => r.Id.Equals(request.Id)).FirstOrDefault();
                currentRequest = request;
                BorrowContext.SaveChanges();
            }
        }
    }
}
