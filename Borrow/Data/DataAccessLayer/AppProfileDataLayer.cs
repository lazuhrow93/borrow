using Borrow.Models.Identity;

namespace Borrow.Data.DataAccessLayer
{
    public class AppProfileDataLayer : Datalayer
    {
        private BorrowContext BorrowContext { get; set; }

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
    }
}