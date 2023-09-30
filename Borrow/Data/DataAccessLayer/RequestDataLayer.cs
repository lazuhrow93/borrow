using Borrow.Models.Backend;
using Borrow.Models.Identity;
using System;

namespace Borrow.Data.DataAccessLayer
{
    public class RequestDataLayer : Datalayer
    {
        public RequestDataLayer(BorrowContext borrowContext)
        {
            BorrowContext = borrowContext;  
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

    }
}
