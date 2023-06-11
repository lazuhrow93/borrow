using AutoMapper;
using Borrow.Migrations;
using Borrow.Models.Backend;
using Borrow.Models.Identity;
using System;
namespace Borrow.Models.Views.Home
{
    public class HomeViewModel
    {
        public string? NeighborhoodName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public HomeViewModel()
        {
        }
    }
}
