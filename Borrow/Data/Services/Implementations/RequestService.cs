using Borrow.Data.Services.Interfaces;
using Borrow.Models.Backend;
using Borrow.Models.Views.Requests;

namespace Borrow.Data.Services.Implementations
{
    public class RequestService : IRequestService
    {
        public RequestService() { }

        public void AcceptRequest(int requestId)
        {
            throw new NotImplementedException();
        }

        public void ConfirmMeetup(int requestId)
        {
            throw new NotImplementedException();
        }

        public bool CreateRequest(CreateRequestViewModel crvm)
        {
            throw new NotImplementedException();
        }

        public void DeclineRequest(int requestId)
        {
            throw new NotImplementedException();
        }

        public CreateRequestViewModel GetCreateRequestViewModel(int listingId, User Requester)
        {
            throw new NotImplementedException();
        }

        public IncomingRequestsViewModel GetIncomingRequestsViewModel(User user)
        {
            throw new NotImplementedException();
        }

        public OutgoingRequestsViewModel GetOutgoingRequestsViewModel(User user)
        {
            throw new NotImplementedException();
        }

        public RequestViewModel GetRequestViewModel(int id)
        {
            throw new NotImplementedException();
        }

        public SetupMeetingViewModel GetSetupMeetingViewModel(int requestId)
        {
            throw new NotImplementedException();
        }

        public void SetUpMeetingSpot(SetupMeetingViewModel meetingInfo)
        {
            throw new NotImplementedException();
        }

        public void UpdateStatus(int requestId, Request.RequestStatus newstatus)
        {
            throw new NotImplementedException();
        }
    }
}
