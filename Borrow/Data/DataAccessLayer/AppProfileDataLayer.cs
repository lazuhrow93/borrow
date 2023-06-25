using Borrow.Models.Identity;

namespace Borrow.Data.DataAccessLayer
{
    public class AppProfileDataLayer
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
    }
}