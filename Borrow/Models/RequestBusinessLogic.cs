using AutoMapper;
using Borrow.Data;
using Borrow.Data.DataAccessLayer;
using Borrow.Data.DataAccessLayer.Interfaces;
using Borrow.Models.Backend;
using Borrow.Models.Identity;
using Borrow.Models.Views.TableViews;

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

        public void CreateRequest(Guid itemIdentifier, User user)
        {
            var item = ItemDataLayer.Get(itemIdentifier);
            var requester = AppProfileDataLayer.Get(user.ProfileId);
            var owner = AppProfileDataLayer.GetByOwnerId(item.OwnerId);

            var newborrowRequest = new Request();
            newborrowRequest.LenderKey = owner.RequestKey;
            newborrowRequest.RequesterKey = requester.RequestKey;
            newborrowRequest.ItemId = item.Id;
            newborrowRequest.CreatedAt = DateTime.UtcNow;
            newborrowRequest.UpdatedAt = DateTime.UtcNow;
            RequestDataLayer.Create(newborrowRequest);
        }

        public void CreateRequest(int itemId, User user)
        {
            var item = ItemDataLayer.Get(itemId);
            var requester = AppProfileDataLayer.Get(user.ProfileId);
            var owner = AppProfileDataLayer.GetByOwnerId(item.OwnerId);

            var newborrowRequest = new Request();
            newborrowRequest.LenderKey = owner.RequestKey;
            newborrowRequest.RequesterKey = requester.RequestKey;
            newborrowRequest.ItemId = item.Id;
            newborrowRequest.CreatedAt = DateTime.UtcNow;
            newborrowRequest.UpdatedAt = DateTime.UtcNow;
            RequestDataLayer.Create(newborrowRequest);
        }

        public IEnumerable<RequestViewModel> GetIncoming(User user)
        {
            var appProfile = AppProfileDataLayer.Get(user.ProfileId);
            var rawRequest = RequestDataLayer.Incoming(appProfile);
            var listOfRequestsViews = new List<RequestViewModel>();
            foreach(var request in rawRequest)
            {
                var item = ItemDataLayer.Get(request.ItemId);
                listOfRequestsViews.Add(new RequestViewModel()
                {
                    Id = request.Id,
                    OwnerUserName = item.UserName,
                    Item = item.Name,
                    CreatedDateUtc = request.CreatedAt,
                    Status = request.Status
                });
            }

            return listOfRequestsViews;
        }

        public IEnumerable<RequestViewModel> GetOutGoing(User user)
        {
            var appProfile = AppProfileDataLayer.Get(user.ProfileId);
            var rawRequest = RequestDataLayer.Outgoing(appProfile);
            var listOfRequestsViews = new List<RequestViewModel>();
            foreach (var request in rawRequest)
            {
                var item = ItemDataLayer.Get(request.ItemId);
                listOfRequestsViews.Add(new RequestViewModel()
                {
                    Id = request.Id,
                    OwnerUserName = item.UserName,
                    Item = item.Name,
                    CreatedDateUtc = request.CreatedAt,
                    Status = request.Status
                });
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
            return new RequestViewModel()
            {
                Id = request.Id,
                Item = item.Name,
                OwnerUserName = item.UserName,
                CreatedDateUtc = request.CreatedAt,
                Status = request.Status
            };
        }
    }
}
