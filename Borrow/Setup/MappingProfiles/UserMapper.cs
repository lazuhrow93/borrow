using AutoMapper;
using Borrow.Models.Views;
using Borrow.Models.Views.Home;
using Borrow.Models.Backend;

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

            CreateMap<User, HomeViewModel>().
                ForMember(dest => dest.FirstName, o => o.MapFrom(src => src.FirstName)).
                ForMember(dest => dest.LastName, o => o.MapFrom(src => src.LastName));

            CreateMap<RegisterViewModel, User>().
                ForMember(dest => dest.FirstName, o => o.MapFrom(src => src.FirstName)).
                ForMember(dest => dest.LastName, o => o.MapFrom(src => src.LastName)).
                ForMember(dest => dest.UserName, o => o.MapFrom(src => src.UserName)).
                ForMember(dest => dest.Email, o => o.MapFrom(src => src.Email)).
                ForMember(dest => dest.PhoneNumber, o => o.MapFrom(src => src.PhoneNumber));
        }
    }
}
