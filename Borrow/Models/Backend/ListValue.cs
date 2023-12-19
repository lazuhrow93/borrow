namespace Borrow.Models.Backend
{
    public class ListValue : Data, IEquatable<ListValue>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }

        public bool Equals(ListValue other)
        {
            if (other is null) return false;
            return this.Value == other.Value;
        }

    }
}
