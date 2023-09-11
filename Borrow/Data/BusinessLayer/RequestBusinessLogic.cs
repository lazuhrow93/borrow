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
            Mapper = mapper;
        }

        public CreateRequestViewModel GetCreateRequestViewModel(int listingId)
        {
            var crvm = new CreateRequestViewModel();

            var listing = ListingsDataLayer.Get(listingId);
            var item = ItemDataLayer.Get(listing.ItemId);
            var owner = item.OwnerId;
            var appProfile = AppProfileDataLayer.GetByOwnerId(owner);

            var listingViewModel = Mapper.Map<ListingViewModel>(listing);
            Mapper.Map(item, listingViewModel);
            Mapper.Map(appProfile, listingViewModel);


            return new CreateRequestViewModel()
            {
                ListingViewModel = listingViewModel
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

            foreach (var request in requests)
            {
                var itemInfo = ItemDataLayer.Get(request.ItemId);
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

        public bool SubmitRequest(CreateRequestViewModel createRequestViewModel, User user)
        {
            var item = ItemDataLayer.Get(createRequestViewModel.ItemInformation.ItemId);
            var Requester = AppProfileDataLayer.Get(user.ProfileId);
            var Lender = AppProfileDataLayer.GetByOwnerId(item.OwnerId);

            var newborrowRequest = new Request();
            newborrowRequest.LenderKey = Lender.RequestKey;
            newborrowRequest.RequesterKey = Requester.RequestKey;
            newborrowRequest.ItemId = item.Id;
            newborrowRequest.Type = createRequestViewModel.RequestType;
            newborrowRequest.Rate = createRequestViewModel.RequestRate;
            newborrowRequest.ReturnDate = createRequestViewModel.ReturnDateUtc;

            var now = DateTime.UtcNow;
            newborrowRequest.TrackingId = Guid.NewGuid();
            newborrowRequest.UpdatedBy = $"CreateRequest(int, RequestType, Decimal, User)";
            newborrowRequest.UpdateDateUtc = now;
            newborrowRequest.CreatedDateUtc = now;
            RequestDataLayer.Create(newborrowRequest);
            return true;

        }

        public void CreateRequest(int itemId, Request.RequestType Type, decimal Rate, DateTime ReturnDate, User user)
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
