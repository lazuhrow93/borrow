namespace Borrow.Models.Views
{
    public class AddItemViewModel
    {
        public NewItemViewModel? NewItemViewModel { get; set; }
        public List<NewItemViewModel>? ItemsToSave { get; set; }
        public int IndexToRemove { get; set; }

        public AddItemViewModel()
        {
            ItemsToSave = new();
        }

        public void Remove(int index)
        {
            ItemsToSave.RemoveAt(index);
        }
    }
}
