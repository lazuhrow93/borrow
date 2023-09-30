namespace Borrow.Models.Views
{
    public class AddItemViewModel
    {
        public NewItemViewModel? NewItemViewModel { get; set; }
        public List<NewItemViewModel>? ItemsToSave { get; set; }

        public AddItemViewModel()
        {
            ItemsToSave = new();
        }
    }
}
