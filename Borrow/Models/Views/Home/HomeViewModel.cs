using AutoMapper;
using Borrow.Migrations;
using Borrow.Models.Identity;
using System;
namespace Borrow.Models.Views.Home
{
    public class HomeViewModel
    {
        private readonly IMapper _mapper;
        public string? NeighborhoodName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public HomeViewModel(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void Map(User user, NeighborhoodEntity neighborhood)
        {
            HomeViewModel x = _mapper.Map<HomeViewModel>(user);
            _mapper.Map<NeighborhoodEntity, HomeViewModel>(neighborhood, x);

            throw new NotImplementedException();
        }
    }
}
