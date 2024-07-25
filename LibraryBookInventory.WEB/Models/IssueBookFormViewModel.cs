namespace LibraryBookInventory.WEB.Models
{
    public class IssueBookFormViewModel
    {
        public int BookId { get; set; }
        public string UserId { get; set; }
        public IEnumerable<IssuedBookViewModel> Books { get; set; } = new HashSet<IssuedBookViewModel>();
        public IEnumerable<UserViewModel> Users { get; set; } = new HashSet<UserViewModel>();
        public DateTime IssuedDate { get; set; } = DateTime.Now;
        public DateTime ReturnDate { get; set; }  = DateTime.Now.AddDays(1);
    }
}
