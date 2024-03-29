﻿using Borrow.Models.Backend;
using Borrow.Models.Views.Profile;
using Borrow.Models.Views.TableViews;
using Borrow.Models.Views;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Borrow.Data.Services.Interfaces
{
    public interface IProfileService
    {
        //item
        public IEnumerable<ItemViewModel> GetUserItems(User user);
        public ItemViewModel GetItem(int id);
        public bool EditItem(EditItemViewModel viewModel);
        public bool CreateItems(User user, AddItemViewModel viewModel);
        public bool DeleteItem(User user, int ids);
        public void DeleteItems(User user, IEnumerable<int> ids);
        public DeleteItemsViewModel GetDeleteItemsViewModel(AppProfile appProfile);
        public ProfileViewModel GetProfileViewModel(User user);
        public EditItemViewModel GetEditItemViewModel(int id);

        //user
        public Task<bool> Register(RegisterViewModel viewModel);
        public Task<SignInResult> Login(LoginViewModel viewModel);
        public Task Logout();
        public bool IsSignedIn(ClaimsPrincipal claimsPrincipal);
        public Task<User> GetCurrentUser(ClaimsPrincipal claimsPrincipal);

        //app profile
        public AppProfile GetByUser(User user);
    }
}
