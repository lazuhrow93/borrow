namespace Borrow.Models.Views.AddItem
{
    public class AddItemViewModel
    {
        public NewItemViewModel NewItemViewModel { get; set; }
        public List<NewItemViewModel> ItemsToSave { get; set; }

        public AddItemViewModel()
        {
            this.ItemsToSave = new List<NewItemViewModel>();
        }
    }
}
