namespace Borrow.Models.Views
{
    public class ProfileViewModel
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Exchanges { get; set; }
        public List<ItemViewModel> OwnerItems { get; set; }
    }
}
