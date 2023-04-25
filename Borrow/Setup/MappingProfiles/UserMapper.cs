using Borrow.Models.Identity;
using AutoMapper;
using Borrow.Models.Views;

namespace Borrow.Setup.MappingProfiles
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, ProfileViewModel>().
                ForMember(dest => dest.FirstName, o => o.MapFrom(src => src.FirstName)).
                ForMember(dest => dest.LastName, o => o.MapFrom(src => src.LastName)).
                ForMember(dest => dest.Username, o => o.MapFrom(src => src.UserName));
        }

    }
}
