namespace Borrow.Models.Views.Requests
{
    public class IncomingRequestsViewModel
    {
        public List<RequestViewModel> PendingRequestViewModels { get; set; }
        public List<RequestViewModel> AcceptedRequestViewModel { get; set; }
    }
}
