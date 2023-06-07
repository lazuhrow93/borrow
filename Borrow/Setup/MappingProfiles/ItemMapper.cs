using AutoMapper;
using Borrow.Models.Listings;
using Borrow.Models.Views;

namespace Borrow.Setup.MappingProfiles
{
    public class ItemMapper : Profile
    {
        public ItemMapper()
        {
            CreateMap<Item, ItemViewModel>()
                .ForMember(dest => dest.Name, o => o.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, o => o.MapFrom(src => src.Description))
                .ForMember(dest => dest.OwnedSince, o => o.MapFrom(src => src.OwnedSince.ToString("MM/dd/yyyy")))
                .ForMember(dest => dest.Identifier, o => o.MapFrom(src => src.Identifier));

            CreateMap<ItemViewModel, Item>()
                .ForMember(dest => dest.Name, o => o.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, o => o.MapFrom(src => src.Description))
                .ForMember(dest => dest.OwnedSince, o => o.MapFrom(src => src.OwnedSince))
                .ForMember(dest => dest.DailyRate, o => o.MapFrom(src => src.DailyRate))
                .ForMember(dest => dest.WeeklyRate, o => o.MapFrom(src => src.WeeklyRate))
                .ForMember(dest => dest.Available, o => o.MapFrom(src => src.Available))
                .ForMember(dest => dest.Identifier, o => o.MapFrom(src => src.Identifier));

            CreateMap<NewItemViewModel, Item>()
                .ForMember(dest => dest.Name, o => o.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, o => o.MapFrom(src => src.Description))
                .ForMember(dest => dest.OwnedSince, o => o.MapFrom(src => src.DateAcquired.ToString("MM/dd/yyyy")));
        }
    }
}
