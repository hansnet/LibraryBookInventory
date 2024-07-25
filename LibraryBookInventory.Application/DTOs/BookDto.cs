namespace LibraryBookInventory.Application.DTOs
{
    public record BookDto
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public string Author { get; init; }
        public string ISBN { get; init; }
        public string PublishedYear { get; init; }
        public string Genre { get; init; }
    }
}
