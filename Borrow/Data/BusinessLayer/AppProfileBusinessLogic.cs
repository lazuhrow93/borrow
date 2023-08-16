using AutoMapper;
using Borrow.Data.DataAccessLayer;
using Borrow.Data.DataAccessLayer.Interfaces;
using Borrow.Models.Identity;

namespace Borrow.Data.BusinessLayer
{
    public class AppProfileBusinessLogic
    {
        public AppProfileDataLayer AppProfileDataLayer { get; set; }
        private IMapper Mapper { get; set; }

        public AppProfileBusinessLogic(IMasterDL masterDL, IMapper mapper)
        {
            AppProfileDataLayer = masterDL.AppProfileDataLayer;
            Mapper = mapper;
        }

        public AppProfile? Get(int id)
        {
            return AppProfileDataLayer.Get(id);
        }

    }
}
