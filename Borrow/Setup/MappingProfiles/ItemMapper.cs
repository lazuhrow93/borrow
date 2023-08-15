using AutoMapper;
using Borrow.Models.Backend;
using Borrow.Models.Views;
using Borrow.Models.Views.TableViews;

namespace Borrow.Setup.MappingProfiles
{
    public class ItemMapper : Profile
    {
        public ItemMapper()
        {
            CreateMap<Item, ItemViewModel>()
                .ForMember(dest => dest.ItemId, o => o.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, o => o.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, o => o.MapFrom(src => src.Description))
                .ForMember(dest => dest.IsListed, o => o.MapFrom(src => src.IsListed))
                .ForMember(dest => dest.OwnedSince, o => o.MapFrom(src => src.OwnedSince.ToString("MM/dd/yyyy")));

            CreateMap<ItemViewModel, Item>()
                .ForMember(dest => dest.Name, o => o.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, o => o.MapFrom(src => src.Description))
                .ForMember(dest => dest.OwnedSince, o => o.MapFrom(src => src.OwnedSince))
                .ForMember(dest => dest.Available, o => o.MapFrom(src => src.Available)); 

            CreateMap<NewItemViewModel, Item>()
                .ForMember(dest => dest.Name, o => o.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, o => o.MapFrom(src => src.Description))
                .ForMember(dest => dest.OwnedSince, o => o.MapFrom(src => src.DateAcquired.ToString("MM/dd/yyyy")));
        }
    }
}
