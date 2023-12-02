using Borrow.Models.Backend;
using Borrow.Models.Views.Requests;
using System;
namespace Borrow.Data.Services.Interfaces
{
    public interface IRequestControllerService
    {
        public void AcceptRequest(int requestId);
        public void ConfirmMeetupTime(int requestId);
        public bool CreateRequest(CreateRequestViewModel viewModel);
        public void DeclineRequest(int requestId);
        public void OfferMeetupTime(SetupMeetingViewModel meetingInfo);
        public void UpdateStatus(int requestId, int newStatusId);
        public void OwnerViewed(int requestId);
        public CreateRequestViewModel GetCreateRequestViewModel(int listingId, User Requester);
        public IncomingRequestsViewModel GetIncomingRequestsViewModel(User user);
        public OutgoingRequestsViewModel GetOutgoingRequestsViewModel(User user);
        public RequestViewModel GetRequestViewModel(int id);
        public SetupMeetingViewModel GetSetupMeetingViewModel(int requestId);
        public RequestViewModel ParseToView(Request request);

    }
}
