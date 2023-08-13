using System.ComponentModel.DataAnnotations.Schema;

namespace Borrow.Models.Identity
{
    public class AppProfile
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string UserName { get; set; }
        public int NeighborhoodId { get; set; }
        public Guid RequestKey { get; set; } = Guid.NewGuid();
    }
}
