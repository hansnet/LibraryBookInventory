using Microsoft.AspNetCore.Identity;

namespace LibraryBookInventory.WEB.Models
{
    public class IssuedBookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
