using Borrow.Models.Backend;
using Borrow.Models.Views;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Borrow.Data.Services.Interfaces
{
    public interface IUserService
    {
        public Task<bool> Register(RegisterViewModel viewModel);
        public Task<SignInResult> Login(LoginViewModel viewModel);
        public Task Logout();
        public bool IsSignedIn(ClaimsPrincipal claimsPrincipal);
        public Task<User> GetCurrentUser(ClaimsPrincipal claimsPrincipal);
    }
}
