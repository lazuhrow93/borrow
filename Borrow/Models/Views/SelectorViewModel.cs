namespace Borrow.Models.Views
{
    public class SelectorViewModel<T>
    {
        public bool IsSelected { get; set; }
        public T Entity { get; set; }
    }
}
