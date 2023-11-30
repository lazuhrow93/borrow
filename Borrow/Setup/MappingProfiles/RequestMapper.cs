using AutoMapper;
using Borrow.Models.Backend;
using Borrow.Models.Views.Requests;

namespace Borrow.Setup.MappingProfiles
{
    public class RequestMapper : Profile
    {
        public RequestMapper()
        {
            CreateMap<Request, RequestViewModel>()
                .ForMember(dest => dest.RequestId, o => o.MapFrom(src => src.Id))
                .ForMember(dest => dest.Rate, o => o.MapFrom(src => src.Rate))
                .ForMember(dest => dest.Periods, o => o.MapFrom(src => src.PayPeriods))
                .ForMember(dest => dest.Term, o => o.MapFrom(src => src.TermId))
                .ForMember(dest => dest.StatusId, o => o.MapFrom(src => src.StatusId))
                .ForMember(dest => dest.PendingActionFromId, o => o.MapFrom(src => src.PendingActionFromId))
                .ForMember(dest => dest.MeetupTime, o => o.MapFrom(src => src.MeetupTime))
                .ForMember(dest => dest.ListingId, o => o.MapFrom(src => src.ListingId));

            CreateMap<Item, RequestViewModel>()
                .ForMember(dest => dest.ItemName, o => o.MapFrom(src => src.Name));

            CreateMap<CreateRequestViewModel, Request>()
                .ForMember(dest => dest.ListingId, o => o.MapFrom(src => src.ListingViewModel.ListingId))
                .ForMember(dest => dest.RequesterKey, o => o.MapFrom(src => src.RequesterKey))
                .ForMember(dest => dest.LenderKey, o => o.MapFrom(src => src.LenderKey))
                .ForMember(dest => dest.ItemId, o => o.MapFrom(src => src.ListingViewModel.ItemId))
                .ForMember(dest => dest.ReturnDate, o => o.MapFrom(src => src.EstimatedReturnDateUtc))
                .ForMember(dest => dest.PayPeriods, o => o.MapFrom(src => src.PayPeriods))
                .ForMember(dest => dest.Rate, o => o.MapFrom(src => src.RequestRate))
                .ForMember(dest => dest.TermId, o => o.MapFrom(src => src.TermId));
        }
    }
}
