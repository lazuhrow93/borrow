using AutoMapper;
using Borrow.Models.Identity;
using Borrow.Models.Views;

namespace Borrow.Setup
{
    public class MapperConfig
    {
        public static Mapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, ProfileViewModel>();
            });

            return new Mapper(config);
        }
    }
}
