using System.ComponentModel.DataAnnotations;

namespace LibraryBookInventory.Domain.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        public string ISBN { get; set; }
        [Required]
        public string PublishedYear { get; set; }
        public string Genre { get; set; }
    }
}
