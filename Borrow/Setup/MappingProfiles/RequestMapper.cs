using AutoMapper;
using Borrow.Models.Backend;
using Borrow.Models.Identity;
using Borrow.Models.Listings;
using Borrow.Models.Views;
using Borrow.Models.Views.TableViews;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Borrow.Setup.MappingProfiles
{
    public class RequestMapper : Profile
    {
        public RequestMapper()
        {
            CreateMap<Request, RequestViewModel>()
                .ForMember(dest => dest.RequestId, o => o.MapFrom(src => src.Id))
                .ForMember(dest => dest.ItemId, o => o.MapFrom(src => src.ItemId))
                .ForMember(dest => dest.RequestRate, o => o.MapFrom(src => src.Rate))
                .ForMember(dest => dest.RequestType, o => o.MapFrom(src => src.Type))
                .ForMember(dest => dest.CounterRate, o => o.MapFrom(src => src.CounterRate))
                .ForMember(dest => dest.CounterType, o => o.MapFrom(src => src.CounterType))
                .ForMember(dest => dest.ReturnDate, o => o.MapFrom(src => src.ReturnDate))
                .ForMember(dest => dest.CreatedDateUtc, o => o.MapFrom(src => src.CreatedDateUtc))
                .ForMember(dest => dest.Status, o => o.MapFrom(src => src.Status));

            CreateMap<Item, RequestViewModel>()
                .ForMember(dest => dest.OwnerUserName, o => o.MapFrom(src => src.UserName))
                .ForMember(dest => dest.ItemName, o => o.MapFrom(src => src.Name));
        }
    }
}
