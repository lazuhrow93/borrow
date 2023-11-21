﻿using AutoMapper;
using Borrow.Data.Repositories;
using Borrow.Data.Repositories.Interfaces;
using Borrow.Data.Services.Interfaces;
using Borrow.Models.Backend;
using Borrow.Models.Views.Requests;
using Borrow.Models.Views.TableViews;

namespace Borrow.Data.Services.Implementations
{
    public class RequestService : IRequestService
    {
        private readonly IRepository<Request> _requestRepository;
        private readonly IRepository<Listing> _listingRepository;
        private readonly IRepository<Item> _itemRepository;
        private readonly IRepository<AppProfile> _appProfileRepository;
        private readonly IMapper _mapper;

        public RequestService(IRepository<Request> requestRepository,
            IRepository<Listing> listingRepository,
            IRepository<Item> itemRepository,
            IRepository<AppProfile> appProfileRepository,
            IMapper mapper)
        {
            _requestRepository = requestRepository;
            _listingRepository = listingRepository;
            _itemRepository = itemRepository;
            _appProfileRepository = appProfileRepository;
            _mapper = mapper;
        }

        public void AcceptRequest(int requestId)
        {
            var request = _requestRepository.GetById(requestId);
            request.Status = Request.RequestStatus.Accepted;

            var listing = _listingRepository.GetById(request.ListingId);
            listing.Active = false;

            var item = _itemRepository.GetById(listing.ItemId);
            item.Available = false;

            _itemRepository.Save();
            _listingRepository.Save();
            _requestRepository.Save();
        }

        public void ConfirmMeetup(int requestId)
        {
            var request = _requestRepository.GetById(requestId);
            request.MeetupTime = request.SuggestedMeetingTime;
            request.Status = Request.RequestStatus.ConfirmedMeetUp;

            _requestRepository.Save();
        }

        public bool CreateRequest(CreateRequestViewModel crvm)
        {
            if (crvm.RequestType.Equals(Request.RequestType.Weekly))
            {
                crvm.EstimatedReturnDateUtc = DateTime.UtcNow.AddDays(7 * crvm.PayPeriods);
                crvm.RequestRate = crvm.ListingViewModel.WeeklyRate;
            }
            else
            {
                crvm.EstimatedReturnDateUtc = DateTime.UtcNow.AddDays(crvm.PayPeriods);
                crvm.RequestRate = crvm.ListingViewModel.DailyRate;
            }

            Request newRequest = _mapper.Map<Request>(crvm);
            newRequest.UpdatedBy = $"Request Business Logic";
            newRequest.CreatedDateUtc = DateTime.UtcNow;
            newRequest.UpdateDateUtc = DateTime.UtcNow;
            newRequest.TrackingId = Guid.NewGuid();

            _requestRepository.Add(newRequest);
            _requestRepository.Save();
            return true;
        }

        public void DeclineRequest(int requestId)
        {
            var request = _requestRepository.GetById(requestId);
            request.Status = Request.RequestStatus.Declined;
            _requestRepository.Save();
        }

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
            var requests = _requestRepository.Query.Where(r=>r.LenderKey == profile.RequestKey);

            IncomingRequestsViewModel irvm = new();
            irvm.IncomingRequestsViewModel = new();

            foreach (var request in requests)
                irvm.IncomingRequestsViewModel.Add(ParseToView(request));

            return irvm;
        }

        public OutgoingRequestsViewModel GetOutgoingRequestsViewModel(User user)
        {
            var profile = _appProfileRepository.GetById(user.ProfileId);
            var requests = _requestRepository.Query.Where(r=>r.RequesterKey == profile.RequestKey);

            OutgoingRequestsViewModel orvm = new();
            orvm.RequestViewModels = new();

            foreach (var request in requests)
            {
                var listingInfo = _listingRepository.GetById(request.ListingId);
                var itemInfo = _itemRepository.GetById(listingInfo.ItemId);
                var requester = _appProfileRepository.Query.First(a=>a.RequestKey == request.RequesterKey);
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

        public void SetUpMeetingSpot(SetupMeetingViewModel meetingInfo)
        {
            var request = _requestRepository.GetById(meetingInfo.RequestId);
            request.SuggestedMeetingTime = meetingInfo.MeetUpTime;
            request.Status = Request.RequestStatus.PendingMeetUp;
            _requestRepository.Save();
        }

        public void UpdateStatus(int requestId, Request.RequestStatus newstatus)
        {
            var request = _requestRepository.GetById(requestId);
            request.Status = newstatus;
            _requestRepository.Save();
        }

        private RequestViewModel ParseToView(Request request)
        {
            var listingInfo = _listingRepository.GetById(request.ListingId);
            var itemInfo = _itemRepository.GetById(listingInfo.ItemId);
            var requester = _appProfileRepository.Query.First(a=>a.RequestKey == request.RequesterKey);
            var lender = _appProfileRepository.Query.First(a => a.RequestKey == request.LenderKey);

            var rvm = _mapper.Map<RequestViewModel>(request);
            _mapper.Map(itemInfo, rvm);
            rvm.Requester = requester.UserName;
            rvm.Lender = lender.UserName;
            return rvm;

        }
    }
}
