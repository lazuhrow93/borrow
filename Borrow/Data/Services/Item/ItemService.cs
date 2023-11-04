using AutoMapper;
using Borrow.Data.Repositories.Interfaces;
using Borrow.Models.Backend;
using Borrow.Models.Views;
using Borrow.Models.Views.Profile;
using Borrow.Models.Views.TableViews;
using System;

namespace Borrow.Data.Services
{
    public class ItemService : IItemService
    {
        public IRepository<Item> _itemRespository { get; set; }
        public IRepository<AppProfile> _appProfileRepository { get; set; }
        public IMapper _mapper { get; set; }

        public ItemService(IRepository<Item> itemRepository, IRepository<AppProfile> appProfileRepository, IMapper mapper)
        {
            _itemRespository = itemRepository;
            _appProfileRepository = appProfileRepository;
            _mapper = mapper;

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

            _itemRespository.Add(newItems);
            _itemRespository.Save();
            return true;
        }

        public IEnumerable<ItemViewModel> GetUserItems(User user)
        {
            var appProfile = _appProfileRepository.Query.Where(a => a.Id.Equals(user.ProfileId)).First();
            var items = _itemRespository.Query.Where(i => i.OwnerId.Equals(appProfile.OwnerId)).ToList();
            return _mapper.Map<IEnumerable<ItemViewModel>>(items);
        }

        public ItemViewModel GetItem(int id)
        {
            var item = _itemRespository.Query.Where(i => i.Id.Equals(id)).FirstOrDefault();
            return _mapper.Map<ItemViewModel>(item);
        }

        public bool EditItem(EditItemViewModel viewModel)
        {
            var updatedItem = _mapper.Map<Item>(viewModel);
            var currentItem = _itemRespository.GetById(updatedItem.Id);

            if (currentItem == null) return false;
            currentItem.Name = updatedItem.Name;
            currentItem.Description = updatedItem.Description;

            _itemRespository.Save();
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
    }
}
