using AutoMapper;
using Borrow.Models.Backend;
using Borrow.Models.Identity;
using Borrow.Models.Views;
using Borrow.Models.Views.Requests;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Borrow.Setup.MappingProfiles
{
    public class RequestMapper : Profile
    {
        public RequestMapper()
        {
            CreateMap<Request, RequestViewModel>()
                .ForMember(dest => dest.RequestId, o => o.MapFrom(src => src.Id))
                .ForMember(dest => dest.ListingId, o => o.MapFrom(src => src.ListingId));

            CreateMap<Item, RequestViewModel>()
                .ForMember(dest => dest.ItemName, o => o.MapFrom(src => src.Name));
        }
    }
}
