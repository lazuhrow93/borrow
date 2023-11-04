using Borrow.Models.Backend;
using System;

namespace Borrow.Data.Repositories
{
    public class RequestDataLayer : Datalayer
    {
        public RequestDataLayer(BorrowContext borrowContext)
        {
            BorrowContext = borrowContext;  
        }

        public Request Get(int id)
        {
            return BorrowContext.Request.Where(r => r.Id.Equals(id)).FirstOrDefault();
        }

        public IEnumerable<Request> GetAllByLender(Guid key)
        {
            var requests = BorrowContext.Request.Where(r=>r.LenderKey == key);
            return requests;
        }

        public IEnumerable<Request> GetAllByRequester(Guid key)
        {
            var requests = BorrowContext.Request.Where(r => r.RequesterKey == key);
            return requests;
        }

        public void Create(Request request)
        {
            BorrowContext.Add(request);
            BorrowContext.SaveChanges();
        }

        internal void Update(Request request)
        {
            var tracker = BorrowContext.Request.Find(request.Id);
            tracker = request;
            
            BorrowContext.SaveChanges();
        }
    }
}
