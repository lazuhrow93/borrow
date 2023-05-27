namespace Borrow.Models.Views.Item
{
    public class AddItemViewModel
    {
        public NewItemViewModel? NewItemViewModel { get; set; }
        public List<NewItemViewModel>? ItemsToSave { get; set; }
        public int IndexToRemove { get; set; }

        public void Remove(int index)
        {
            this.ItemsToSave.RemoveAt(index);
        }
    }
}
