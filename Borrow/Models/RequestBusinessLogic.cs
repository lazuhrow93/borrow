using AutoMapper;
using Borrow.Data;
using Borrow.Data.DataAccessLayer;
using Borrow.Data.DataAccessLayer.Interfaces;
using Borrow.Models.Backend;
using Borrow.Models.Identity;
using Borrow.Models.Views.TableViews;
using Borrow.Models.Backend;
using Borrow.Models.Listings;

namespace Borrow.Models
{
    public class RequestBusinessLogic
    {
        public RequestDataLayer RequestDataLayer { get; set; }
        public ItemDataLayer ItemDataLayer { get; set; }
        public AppProfileDataLayer AppProfileDataLayer { get;set; }
        private IMapper Mapper { get; set; }   

        public RequestBusinessLogic(IMasterDL masterDL, IMapper mapper)
        {
            RequestDataLayer = masterDL.RequestDataLayer;
            ItemDataLayer = masterDL.ItemDataLayer;
            AppProfileDataLayer = masterDL.AppProfileDataLayer;
            Mapper = mapper;
        }

        public void CreateRequest(int itemId, Request.RequestType Type, Decimal Rate, DateTime ReturnDate, User user)
        {
            var item = ItemDataLayer.Get(itemId);
            var Requester = AppProfileDataLayer.Get(user.ProfileId);
            var Lender = AppProfileDataLayer.GetByOwnerId(item.OwnerId);

            var newborrowRequest = new Request();
            newborrowRequest.LenderKey = Lender.RequestKey;
            newborrowRequest.RequesterKey = Requester.RequestKey;
            newborrowRequest.ItemId = item.Id;
            newborrowRequest.Type = Type;
            newborrowRequest.Rate = Rate;
            newborrowRequest.ReturnDate = ReturnDate;

            var now = DateTime.UtcNow;
            newborrowRequest.TrackingId = Guid.NewGuid();
            newborrowRequest.UpdatedBy = $"CreateRequest(int, RequestType, Decimal, User)";
            newborrowRequest.UpdateDateUtc = now;
            newborrowRequest.CreatedDateUtc = now;
            RequestDataLayer.Create(newborrowRequest);
        }

        public IEnumerable<RequestViewModel> GetIncoming(User user)
        {
            var appProfile = AppProfileDataLayer.Get(user.ProfileId);
            var rawRequests = RequestDataLayer.Incoming(appProfile);
            var listOfRequestsViews = new List<RequestViewModel>();
            foreach(var rawRequest in rawRequests)
            {
                var item = ItemDataLayer.Get(rawRequest.ItemId);

                var mapperrvm = Mapper.Map<Request, RequestViewModel>(rawRequest);
                Mapper.Map<Item, RequestViewModel>(item, mapperrvm);
                listOfRequestsViews.Add(mapperrvm);
            }

            return listOfRequestsViews;
        }

        public IEnumerable<RequestViewModel> GetOutGoing(User user)
        {
            var appProfile = AppProfileDataLayer.Get(user.ProfileId);
            var rawRequests = RequestDataLayer.Outgoing(appProfile);
            var listOfRequestsViews = new List<RequestViewModel>();
            foreach (var rawRequest in rawRequests)
            {
                var item = ItemDataLayer.Get(rawRequest.ItemId);

                var mapperrvm = Mapper.Map<Request, RequestViewModel>(rawRequest);
                Mapper.Map<Item, RequestViewModel>(item, mapperrvm);

                listOfRequestsViews.Add(mapperrvm);
            }

            return listOfRequestsViews;
        }

        public void UpdateStatus(int requestId, Request.RequestStatus newStatus)
        {
            var request = RequestDataLayer.Get(requestId);
            request.Status = newStatus;
            RequestDataLayer.Update(request);
        }

        public void UpdateStatus(IEnumerable<int> ids, Request.RequestStatus newStatus)
        {
            var requests = RequestDataLayer.Get(ids).ToList();
            for(int i = 0; i< requests.Count; i++)
                requests[i].UpdateStatus(newStatus);
            RequestDataLayer.Update(requests);
        }

        public RequestViewModel GetRequest(int requestId)
        {
            var request = RequestDataLayer.Get(requestId);
            var item = ItemDataLayer.Get(request.ItemId);
            var rvm = Mapper.Map<Request, RequestViewModel>(request);
            Mapper.Map<Item, RequestViewModel>(item, rvm);
            return rvm;
        }

        public void CounterOfferRequest(int requestId, Request.RequestType NewRate, decimal NewMoney)
        {
            var request = RequestDataLayer.Get(requestId);
            request.CounterRate = NewMoney;
            request.CounterType = NewRate;
            request.UpdateStatus(Request.RequestStatus.OwnerCounter);
            RequestDataLayer.Update(request);
        }

        internal void RequesterCounterOfferRequest(int requestId, Request.RequestType counterRate, decimal counterMoney)
        {
            var request = RequestDataLayer.Get(requestId);
            request.Type = counterRate;
            request.Rate = counterMoney;
            request.UpdateStatus(Request.RequestStatus.RequesterCounter);
            RequestDataLayer.Update(request);
        }
    }
}
