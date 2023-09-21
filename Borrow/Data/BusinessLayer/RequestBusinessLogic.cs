using AutoMapper;
using Borrow.Data.DataAccessLayer;
using Borrow.Data.DataAccessLayer.Interfaces;
using Borrow.Models.Backend;
using Borrow.Models.Identity;
using Borrow.Models.Views.Requests;
using Borrow.Models.Views.TableViews;

namespace Borrow.Data.BusinessLayer
{
    public class RequestBusinessLogic
    {
        public RequestDataLayer RequestDataLayer { get; set; }
        public ItemDataLayer ItemDataLayer { get; set; }
        public AppProfileDataLayer AppProfileDataLayer { get; set; }
        public ListingsDataLayer ListingsDataLayer { get; set; }
        private IMapper Mapper { get; set; }

        public RequestBusinessLogic(IMasterDL masterDL, IMapper mapper)
        {
            RequestDataLayer = masterDL.RequestDataLayer;
            ItemDataLayer = masterDL.ItemDataLayer;
            AppProfileDataLayer = masterDL.AppProfileDataLayer;
            ListingsDataLayer = masterDL.ListingsDataLayer;
            Mapper = mapper;
        }

        public CreateRequestViewModel GetCreateRequestViewModel(int listingId, User Requester)
        {
            var crvm = new CreateRequestViewModel();

            var listing = ListingsDataLayer.Get(listingId);
            var item = ItemDataLayer.Get(listing.ItemId);
            var owner = item.OwnerId;
            var ownerAppProfile = AppProfileDataLayer.GetByOwnerId(owner);
            var requesterAppProfile = AppProfileDataLayer.Get(Requester.ProfileId);

            var listingViewModel = Mapper.Map<ListingViewModel>(listing);
            Mapper.Map(item, listingViewModel);
            Mapper.Map(ownerAppProfile, listingViewModel);


            return new CreateRequestViewModel()
            {
                ListingViewModel = listingViewModel,
                LenderKey = ownerAppProfile.RequestKey,
                RequesterKey = requesterAppProfile.RequestKey
            };
        }

        public IncomingRequestsViewModel GetIncomingRequestsViewModel(User user)
        {
            var profile = AppProfileDataLayer.Get(user.ProfileId);
            var requests = RequestDataLayer.GetAllByLender(profile.RequestKey);

            IncomingRequestsViewModel incomingRequestsViewModel = new();

            foreach(var request in requests)
            {
                var itemInfo = ItemDataLayer.Get(request.ItemId);
                var requester = AppProfileDataLayer.GetByRequestKey(request.RequesterKey);
                var rvm = Mapper.Map<RequestViewModel>(request);
                Mapper.Map(itemInfo, rvm);
                rvm.Requester = requester.UserName;
                rvm.Lender = user.UserName;
                incomingRequestsViewModel.RequestViewModels.Add(rvm);
            }
            return incomingRequestsViewModel;
        }

        public OutgoingRequestsViewModel GetOutgoingRequestsViewModel(User user)
        {
            var profile = AppProfileDataLayer.Get(user.ProfileId);
            var requests = RequestDataLayer.GetAllByRequester(profile.RequestKey);

            OutgoingRequestsViewModel orvm = new();
            orvm.RequestViewModels = new();

            foreach (var request in requests)
            {
                var listingInfo = ListingsDataLayer.Get(request.ListingId);
                var itemInfo = ItemDataLayer.Get(listingInfo.ItemId);
                var requester = AppProfileDataLayer.Get(user.ProfileId);
                var lender = AppProfileDataLayer.GetByRequestKey(request.LenderKey);

                var rvm = Mapper.Map<RequestViewModel>(request);
                Mapper.Map(itemInfo, rvm);
                rvm.Requester = requester.UserName;
                rvm.Lender = lender.UserName;
                orvm.RequestViewModels.Add(rvm);
            }
            return orvm;

        }

        public bool CreateRequest(CreateRequestViewModel crvm)
        {
            if (crvm.RequestType.Equals(Request.RequestType.Weekly))
                crvm.EstimatedReturnDateUtc = DateTime.UtcNow.AddDays(7 * crvm.PayPeriods);
            else
                crvm.EstimatedReturnDateUtc = DateTime.UtcNow.AddDays(crvm.PayPeriods);
            
            Request request = Mapper.Map<Request>(crvm);
            request.UpdatedBy = $"Request Business Logic";
            request.CreatedDateUtc = DateTime.UtcNow;
            request.UpdateDateUtc = DateTime.UtcNow;
            request.TrackingId = Guid.NewGuid();
            
            
            RequestDataLayer.Create(request);
            return true;
        }

        public IEnumerable<(Request, Item)> GetIncoming(User user)
        {
            var appProfile = AppProfileDataLayer.Get(user.ProfileId);
            var requestItem = new List<(Request request, Item item)>();
            var rawRequests = RequestDataLayer.Incoming(appProfile);



            foreach (var rawRequest in rawRequests)
            {
                var item = ItemDataLayer.Get(rawRequest.ItemId);
                requestItem.Add((rawRequest, item));
            }

            return requestItem;
        }

        public IEnumerable<(Request, Item)> GetOutgoing(User user)
        {
            var appProfile = AppProfileDataLayer.Get(user.ProfileId);
            var requestItem = new List<(Request request, Item item)>();
            var rawRequests = RequestDataLayer.Outgoing(appProfile);



            foreach (var rawRequest in rawRequests)
            {
                var item = ItemDataLayer.Get(rawRequest.ItemId);
                requestItem.Add((rawRequest, item));
            }

            return requestItem;
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
            for (int i = 0; i < requests.Count; i++)
                requests[i].UpdateStatus(newStatus);
            RequestDataLayer.Update(requests);
        }

        public (Request Request, Item Item) GetRequest(int requestId)
        {
            var request = RequestDataLayer.Get(requestId);
            var item = ItemDataLayer.Get(request.ItemId);
            //var rvm = Mapper.Map<Request, RequestViewModel>(request);
            //Mapper.Map<Item, RequestViewModel>(item, rvm);
            return (request, item);
        }

        public void OwnerAcceptRequest(int requestId)
        {
            var request = RequestDataLayer.Get(requestId);
            var item = ItemDataLayer.Get(request.ItemId);
            item.Available = false;
            ItemDataLayer.Update(item);
            request.UpdateStatus(Request.RequestStatus.Accepted);
            RequestDataLayer.Update(request);
        }

        internal void DeclineRequest(int requestId)
        {
            var request = RequestDataLayer.Get(requestId);
            request.UpdateStatus(Request.RequestStatus.Declined);
            RequestDataLayer.Update(request);
        }

    }
}
