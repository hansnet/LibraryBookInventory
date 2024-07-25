namespace LibraryBookInventory.Application.DTOs
{
    public class IssueBookDto
    {
        public int BookId { get; set; }
        public string UserId { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
