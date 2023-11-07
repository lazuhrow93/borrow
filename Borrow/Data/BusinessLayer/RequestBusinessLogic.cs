using AutoMapper;
using Borrow.Data.Repositories;
using Borrow.Data.Repositories.Interfaces;
using Borrow.Models.Backend;
using Borrow.Models.Views.Requests;
using Borrow.Models.Views.TableViews;
using NuGet.Protocol;
using System.Data;

namespace Borrow.Data.BusinessLayer
{
    public class RequestBusinessLogic
    {
        public RequestDataLayer RequestDataLayer { get; set; }
        public ItemDataLayer ItemDataLayer { get; set; }
        public AppProfileDataLayer AppProfileDataLayer { get; set; }
        public ListingsDataLayer ListingsDataLayer { get; set; }
        private IMapper Mapper { get; set; }

        #region Parse To Views

        public RequestBusinessLogic(IMasterDL masterDL, IMapper mapper)
        {
            RequestDataLayer = masterDL.RequestDataLayer;
            ItemDataLayer = masterDL.ItemDataLayer;
            AppProfileDataLayer = masterDL.AppProfileDataLayer;
            ListingsDataLayer = masterDL.ListingsDataLayer;
            Mapper = mapper;
        }

        public RequestViewModel GetRequestViewModel(int id)
        {
            return ParseToView(RequestDataLayer.Get(id));
        }

        public SetupMeetingViewModel GetSetupMeetingViewModel(int requestId)
        {
            return new SetupMeetingViewModel()
            {
                MeetUpTime = DateTime.UtcNow,
                RequestId = requestId
            };
        }

        private RequestViewModel ParseToView(Request request)
        {
            var listingInfo = ListingsDataLayer.Get(request.ListingId);
            var itemInfo = ItemDataLayer.Get(listingInfo.ItemId);
            var requester = AppProfileDataLayer.GetByRequestKey(request.RequesterKey);
            var lender = AppProfileDataLayer.GetByRequestKey(request.LenderKey);

            var rvm = Mapper.Map<RequestViewModel>(request);
            Mapper.Map(itemInfo, rvm);
            rvm.Requester = requester.UserName;
            rvm.Lender = lender.UserName;
            return rvm;

        }

        #endregion

        #region Logic

        public void UpdateStatus(int requestId, Request.RequestStatus newstatus)
        {
            var request = RequestDataLayer.Get(requestId);
            request.Status = newstatus;
            RequestDataLayer.Update(request);
        }

        public void SetUpMeetingSpot(SetupMeetingViewModel meetingInfo)
        {
            var request = RequestDataLayer.Get(meetingInfo.RequestId);
            request.SuggestedMeetingTime = meetingInfo.MeetUpTime;
            request.Status = Request.RequestStatus.PendingMeetUp;
            RequestDataLayer.Update(request);
        }

        #endregion
    }
}
