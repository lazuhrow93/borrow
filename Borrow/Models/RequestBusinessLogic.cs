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

            var newborrowRequest = new BorrowRequest();
            newborrowRequest.OwnerKey = owner.RequestKey;
            newborrowRequest.RequesterKey = requester.RequestKey;
            newborrowRequest.ItemId = item.Id;
            newborrowRequest.CreatedAt = DateTime.UtcNow;
            newborrowRequest.UpdatedAt = DateTime.UtcNow;
            RequestDataLayer.Create(newborrowRequest);
        }

        public IEnumerable<BorrowRequestViewModel> GetIncoming(User user)
        {
            var appProfile = AppProfileDataLayer.Get(user.ProfileId);
            var rawRequest = RequestDataLayer.Incoming(appProfile);
            var listOfRequestsViews = new List<BorrowRequestViewModel>();
            foreach(var request in rawRequest)
            {
                var item = ItemDataLayer.Get(request.ItemId);
                listOfRequestsViews.Add(new BorrowRequestViewModel()
                {
                    OwnerUserName = item.UserName,
                    Item = item.Name,
                    CreatedDateUtc = request.CreatedAt,
                    Status = request.Status
                });
            }

            return listOfRequestsViews;
        }

        public IEnumerable<BorrowRequestViewModel> GetOutGoing(User user)
        {
            var appProfile = AppProfileDataLayer.Get(user.ProfileId);
            var rawRequest = RequestDataLayer.Outgoing(appProfile);
            var listOfRequestsViews = new List<BorrowRequestViewModel>();
            foreach (var request in rawRequest)
            {
                var item = ItemDataLayer.Get(request.ItemId);
                listOfRequestsViews.Add(new BorrowRequestViewModel()
                {
                    OwnerUserName = item.UserName,
                    Item = item.Name,
                    CreatedDateUtc = request.CreatedAt,
                    Status = request.Status
                });
            }

            return listOfRequestsViews;
        }
    }
}
