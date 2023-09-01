using Borrow.Models.Backend;
using Borrow.Models.Views.TableViews;
using AutoMapper;
using Borrow.Models.Identity;

namespace Borrow.Setup.MappingProfiles
{
    public class ListingMapper : Profile
    {
        public ListingMapper()
        {
            CreateMap<Item, ListingViewModel>()
                .ForMember(dest => dest.ItemId, o => o.MapFrom(src => src.Id))
                .ForMember(dest => dest.ItemName, o => o.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, o => o.MapFrom(src => src.Description))
                .ForMember(dest => dest.OwnerId, o => o.MapFrom(src => src.OwnerId));

            CreateMap<Listing, ListingViewModel>()
                .ForMember(dest => dest.ListingId, o => o.MapFrom(src => src.Id))
                .ForMember(dest => dest.DailyRate, o => o.MapFrom(src => src.DailyRate))
                .ForMember(dest => dest.WeeklyRate, o => o.MapFrom(src => src.WeeklyRate));

            CreateMap<AppProfile, ListingViewModel>()
                .ForMember(dest => dest.OwnerUsername, o => o.MapFrom(src => src.UserName));
        }
    }
}
