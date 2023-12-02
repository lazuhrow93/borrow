using AutoMapper;
using Borrow.Data.Repositories.Implementations;
using Borrow.Data.Repositories.Interfaces;
using Borrow.Data.Services.Interfaces;
using Borrow.Models.Backend;
using Borrow.Models.Views;
using Borrow.Models.Views.Profile;
using Borrow.Models.Views.TableViews;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Borrow.Data.Services.Implementations
{
    public partial class ProfileControllerService : IProfileControllerService
    {
        private IRepository<Item> _itemRepository;
        private IRepository<AppProfile> _appProfileRepository;
        private IRepository<Neighborhood> _neighborhoodRepository;
        private IRepository<User> _userRepository;
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        public IMapper _mapper;

        public ProfileControllerService(IRepository<Item> itemrepo,
            IRepository<AppProfile> appProfileRepo,
            IRepository<Neighborhood> neighborhoodRepo,
            IRepository<User> userRepo,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IMapper mapper
            )
        {
            _mapper = mapper;
            _neighborhoodRepository = neighborhoodRepo;
            _userManager = userManager;
            _signInManager = signInManager;
            _userRepository = userRepo;
            _appProfileRepository = appProfileRepo;
        }

        public bool CreateItems(User user, AddItemViewModel viewModel)
        {
            var appProfile = _appProfileRepository.Query.Where(a => a.Id.Equals(user.ProfileId)).First();
            var newItems = new List<Item>();
            foreach (var item in viewModel.ItemsToSave)
            {
                var newItem = _mapper.Map<Item>(item);
                newItem.Available = false;
                newItem.OwnerId = appProfile.OwnerId;
                newItem.IsListed = false;
                newItem.NeighborhoodId = appProfile.NeighborhoodId;
                newItems.Add(newItem);
            }

            _itemRepository.Add(newItems);
            _itemRepository.Save();
            return true;
        }

        public bool DeleteItem(User user, int ids)
        {
            throw new NotImplementedException();
        }

        public bool DeleteItems(User user, IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public bool EditItem(EditItemViewModel viewModel)
        {
            var updatedItem = _mapper.Map<Item>(viewModel);
            var currentItem = _itemRepository.GetById(updatedItem.Id);

            if (currentItem == null) return false;
            currentItem.Name = updatedItem.Name;
            currentItem.Description = updatedItem.Description;

            _itemRepository.Save();
            return true;
        }

        public AppProfile? GetByUser(User user)
        {
            return _appProfileRepository.GetById(user.ProfileId);
        }

        public Task<User> GetCurrentUser(ClaimsPrincipal claimsPrincipal)
        {
            return _userManager.GetUserAsync(claimsPrincipal);
        }

        public IEnumerable<ItemViewModel> GetUserItems(User user)
        {
            var appProfile = _appProfileRepository.Query.Where(a => a.Id.Equals(user.ProfileId)).First();
            var items = _itemRepository.Query.Where(i => i.OwnerId.Equals(appProfile.OwnerId)).ToList();
            return _mapper.Map<IEnumerable<ItemViewModel>>(items);
        }

        public bool IsSignedIn(ClaimsPrincipal claimsPrincipal)
        {
            return _signInManager.IsSignedIn(claimsPrincipal);
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
    }
}
