using Borrow.Models.Backend;
using Borrow.Models.Views.Requests;
using Borrow.Data.Services.Interfaces;
using Borrow.Models.Views.TableViews;

namespace Borrow.Data.Services.Implementations
{
    public partial class RequestService : IRequestService
    {
        public CreateRequestViewModel GetCreateRequestViewModel(int listingId, User Requester)
        {
            var crvm = new CreateRequestViewModel();

            var listing = _listingRepository.GetById(listingId);
            var item = _itemRepository.GetById(listing.ItemId);
            var owner = item.OwnerId;
            var ownerAppProfile = _appProfileRepository.Query.First(a => a.OwnerId == owner);
            var requesterAppProfile = _appProfileRepository.Query.First(a => a.Id == Requester.ProfileId);

            var listingViewModel = _mapper.Map<ListingViewModel>(listing);
            _mapper.Map(item, listingViewModel);
            _mapper.Map(ownerAppProfile, listingViewModel);


            return new CreateRequestViewModel()
            {
                ListingViewModel = listingViewModel,
                LenderKey = ownerAppProfile.RequestKey,
                RequesterKey = requesterAppProfile.RequestKey
            };
        }

        public IncomingRequestsViewModel GetIncomingRequestsViewModel(User user)
        {
            var profile = _appProfileRepository.GetById(user.ProfileId);
            var requests = _requestRepository.Query.Where(r => r.LenderKey == profile.RequestKey);

            IncomingRequestsViewModel irvm = new();
            irvm.RequestViewModels = new();

            foreach (var request in requests)
                irvm.RequestViewModels.Add(ParseToView(request));

            return irvm;
        }

        public OutgoingRequestsViewModel GetOutgoingRequestsViewModel(User user)
        {
            var profile = _appProfileRepository.GetById(user.ProfileId);
            var requests = _requestRepository.Query.Where(r => r.RequesterKey == profile.RequestKey);

            OutgoingRequestsViewModel orvm = new();
            orvm.RequestViewModels = new();

            foreach (var request in requests)
            {
                var listingInfo = _listingRepository.GetById(request.ListingId);
                var itemInfo = _itemRepository.GetById(listingInfo.ItemId);
                var requester = _appProfileRepository.Query.First(a => a.RequestKey == request.RequesterKey);
                var lender = _appProfileRepository.Query.First(a => a.RequestKey == request.LenderKey);

                var rvm = _mapper.Map<RequestViewModel>(request);
                _mapper.Map(itemInfo, rvm);
                rvm.Requester = requester.UserName;
                rvm.Lender = lender.UserName;
                orvm.RequestViewModels.Add(rvm);
            }
            return orvm;
        }

        public RequestViewModel GetRequestViewModel(int id)
        {
            return ParseToView(_requestRepository.GetById(id));
        }

        public SetupMeetingViewModel GetSetupMeetingViewModel(int requestId)
        {
            return new SetupMeetingViewModel()
            {
                MeetUpTime = DateTime.UtcNow,
                RequestId = requestId
            };
        }

        public RequestViewModel ParseToView(Request request)
        {
            var listingInfo = _listingRepository.GetById(request.ListingId);
            var itemInfo = _itemRepository.GetById(listingInfo.ItemId);
            var requester = _appProfileRepository.Query.First(a => a.RequestKey == request.RequesterKey);
            var lender = _appProfileRepository.Query.First(a => a.RequestKey == request.LenderKey);

            var rvm = _mapper.Map<RequestViewModel>(request);
            _mapper.Map(itemInfo, rvm);
            rvm.Requester = requester.UserName;
            rvm.Lender = lender.UserName;
            return rvm;
        }
    }
}
