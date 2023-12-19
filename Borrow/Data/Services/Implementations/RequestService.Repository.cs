﻿using AutoMapper;
using Borrow.Data.Repositories.Interfaces;
using Borrow.Data.Services.Interfaces;
using Borrow.Models.Backend;
using Borrow.Models.Enums;
using Borrow.Models.Views.Requests;

namespace Borrow.Data.Services.Implementations
{
    public partial class RequestService : IRequestService
    {
        private readonly IRepository<Request> _requestRepository;
        private readonly IRepository<Listing> _listingRepository;
        private readonly IRepository<Item> _itemRepository;
        private readonly IRepository<AppProfile> _appProfileRepository;
        private readonly IRepository<ListValue> _listValueRepository;
        private readonly IMapper _mapper;

        public RequestService(IRepository<Request> requestRepo,
            IRepository<Listing> listingRepo,
            IRepository<Item> itemRepo,
            IRepository<AppProfile> appProfileRepo,
            IRepository<ListValue> borrowRepo,
            IMapper mapper)
        {
            _requestRepository = requestRepo;
            _listingRepository = listingRepo;
            _itemRepository = itemRepo;
            _appProfileRepository = appProfileRepo;
            _listValueRepository = borrowRepo;
            _mapper = mapper;
        }

        public void AcceptRequest(int requestId)
        {
            var acceptedId = _listValueRepository.GetById((int)RequestEnums.Status.Accepted).Id;
            UpdateStatus(requestId, acceptedId);
            var request = _requestRepository.GetById(requestId);
            //request.Status = Request.RequestStatus.Accepted;

            var listing = _listingRepository.GetById(request.ListingId);
            listing.Active = false;

            var item = _itemRepository.GetById(listing.ItemId);
            item.Available = false;

            _itemRepository.Save();
            _listingRepository.Save();
            _requestRepository.Save();
        }

        public void ConfirmMeetupTime(int requestId)
        {
            throw new NotImplementedException();
        }

        public bool CreateRequest(CreateRequestViewModel viewModel)
        {
            var weekly = _listValueRepository.GetById((int)RequestEnums.Term.Weekly);
            var selectedListValue = new ListValue()
            {
                Value = (int)viewModel.TermId
            };

            if (selectedListValue.Equals(weekly))
            {
                viewModel.EstimatedReturnDateUtc = DateTime.UtcNow.AddDays(7 * viewModel.PayPeriods);
                viewModel.RequestRate = viewModel.ListingViewModel.WeeklyRate;
            }
            else
            {
                viewModel.EstimatedReturnDateUtc = DateTime.UtcNow.AddDays(viewModel.PayPeriods);
                viewModel.RequestRate = viewModel.ListingViewModel.DailyRate;
            }

            Request newRequest = _mapper.Map<Request>(viewModel);
            newRequest.UpdatedBy = $"Request Business Logic";
            newRequest.CreatedDateUtc = DateTime.UtcNow;
            newRequest.UpdateDateUtc = DateTime.UtcNow;
            newRequest.StatusId = (int)RequestEnums.Status.Pending;
            newRequest.TrackingId = Guid.NewGuid();

            _requestRepository.Add(newRequest);
            _requestRepository.Save();
            return true;
        }

        public void DeclineRequest(int requestId)
        {
            var deletelistValue = _listValueRepository.GetById((int)RequestEnums.Status.Declined);
            UpdateStatus(requestId, deletelistValue.Value);
        }

        public void OfferMeetupTime(SetupMeetingViewModel viewModel)
        {
            var request = _requestRepository.GetById(viewModel.RequestId);
            request.RequesterSuggestedMeetingTime = viewModel.MeetUpTime;
            var newEnum = _listValueRepository.GetById((int)RequestEnums.PendingActionFrom.Lender);
            request.PendingActionFromId = newEnum.Id;
            _requestRepository.Save();
        }

        public void OwnerViewed(int requestId)
        {
            var newStatus = _listValueRepository.GetById((int)RequestEnums.Status.Viewed);
            var currentRequest = _requestRepository.GetById(requestId);

            if (currentRequest.StatusId > newStatus.Value) return;

            UpdateStatus(requestId, newStatus.Value);
        }


        public void UpdateStatus(int requestId, int newStatusId)
        {
            var request = _requestRepository.GetById(requestId);
            request.StatusId = newStatusId;
            _requestRepository.Save();
        }
    }
}
