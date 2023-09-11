using Borrow.Models.Identity;

namespace Borrow.Data.DataAccessLayer
{
    public class AppProfileDataLayer : Datalayer
    {
        public AppProfileDataLayer(BorrowContext borrowContext)
        {
            BorrowContext = borrowContext;
        }

        public AppProfile? Get(int profileId)
        {
            return BorrowContext.AppProfile.Where(a => a.Id.Equals(profileId)).FirstOrDefault();
        }

        public AppProfile? GetByOwnerId(int ownerid)
        {
            return BorrowContext.AppProfile.Where(a=>a.OwnerId.Equals(ownerid)).FirstOrDefault(); //dont like this
        }

        public AppProfile? GetByRequestKey(Guid key)
        {
            return BorrowContext.AppProfile.Where(a=>a.RequestKey.Equals(key)).FirstOrDefault();
        }
    }
}