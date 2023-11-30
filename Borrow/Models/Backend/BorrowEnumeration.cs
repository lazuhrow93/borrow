using Borrow.Migrations;
using System;
namespace Borrow.Models.Backend
{
    public class BorrowEnumeration : Data, IEquatable<BorrowEnumeration>
    {
        public int Id { get; set; }
        public string Name { get; }
        public int Value { get; }

        public bool Equals(BorrowEnumeration? other)
        {
            if (other is null) return false;
            return (other.Value == Value);
        }
    }
}

