using AutoMapper;
using Borrow.Data.Repositories.Interfaces;
using Borrow.Data.Services.Interfaces;
using Borrow.Models.Backend;
using Borrow.Models.Views;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Security.Policy;

namespace Borrow.Data.Services.Implementations
{
    public class UserService : IUserService
    {
        private IRepository<Neighborhood> _neighborhoodRepository;
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private IRepository<AppProfile> _appProfileRepository;
        private IRepository<User> _userRepository;
        private IMapper _mapper;

        public UserService(
            IMapper mapper,
            IRepository<Neighborhood> neighborhoodRepository,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IRepository<User> userRepository,
            IRepository<AppProfile> appProfileRepository) 
        {
            _mapper = mapper;
            _neighborhoodRepository = neighborhoodRepository; 
            _userManager = userManager;
            _signInManager = signInManager;
            _userRepository = userRepository;
            _appProfileRepository = appProfileRepository;
        }

        public async Task<bool> Register(RegisterViewModel viewModel)
        {
            var neighborhood = _neighborhoodRepository.GetById(viewModel.Neighborhood);
            if (neighborhood == null) return false;

            var newUser = _mapper.Map<User>(viewModel);
            var successCreate = await _userManager.CreateAsync(newUser, viewModel.Password);

            if (successCreate.Succeeded == false) return false;
            var signIn = _signInManager.SignInAsync(newUser, isPersistent: false);

            //creeate app profile
            var maxOwnerId = _appProfileRepository.Query.Max(p => p.OwnerId);
            var newAppProfile = new AppProfile()
            {
                NeighborhoodId = neighborhood.Id,
                OwnerId = maxOwnerId + 1
            };

            var appProfile = _appProfileRepository.Add(newAppProfile);
            _appProfileRepository.Save();
            await signIn; //wait till sign in finish

            var user = _userRepository.GetById(Int32.Parse(newUser.Id));
            user.ProfileId = appProfile.Id;
            _userRepository.Save();
            return true;
        }

        public Task<SignInResult> Login(LoginViewModel viewModel)
        {
            return _signInManager
                .PasswordSignInAsync(viewModel.UserName, viewModel.PasswordHash, isPersistent: viewModel.RememberMe, lockoutOnFailure: false);
        }

        public Task Logout()
        {
            return _signInManager.SignOutAsync();
        }

        public bool IsSignedIn(ClaimsPrincipal claimsPrincipal)
        {
            return _signInManager.IsSignedIn(claimsPrincipal);
        }

        public Task<User> GetCurrentUser(ClaimsPrincipal claimsPrincipal)
        {
            return _userManager.GetUserAsync(claimsPrincipal);
        }


    }
}
