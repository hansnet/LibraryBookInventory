
using Microsoft.AspNetCore.Identity;

namespace LibraryBookInventory.Domain.Entities
{
    public class IssuedBook
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
