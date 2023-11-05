using Borrow.Models.Backend;
using Borrow.Models.Views.Requests;

namespace Borrow.Data.Services.Interfaces
{
    public interface IRequestService
    {
        public CreateRequestViewModel GetCreateRequestViewModel(int listingId, User Requester);
        public IncomingRequestsViewModel GetIncomingRequestsViewModel(User user);
        public OutgoingRequestsViewModel GetOutgoingRequestsViewModel(User user);
        public RequestViewModel GetRequestViewModel(int id);
        public SetupMeetingViewModel GetSetupMeetingViewModel(int requestId);
        public bool CreateRequest(CreateRequestViewModel crvm);
        public void AcceptRequest(int requestId);
        public void DeclineRequest(int requestId);
        public void UpdateStatus(int requestId, Request.RequestStatus newstatus);
        public void SetUpMeetingSpot(SetupMeetingViewModel meetingInfo);
        public void ConfirmMeetup(int requestId);
    }
}
