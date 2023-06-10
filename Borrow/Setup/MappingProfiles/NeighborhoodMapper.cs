using AutoMapper;
using Borrow.Migrations;
using Borrow.Models.Backend;
using Borrow.Models.Views.Home;

namespace Borrow.Setup.MappingProfiles
{
    public class NeighborhoodMapper : Profile
    {
        public NeighborhoodMapper()
        {
            CreateMap<Neighborhood, HomeViewModel>().
                    ForMember(dest => dest.NeighborhoodName, o => o.MapFrom(src => src.Name));
        }
    }
}
