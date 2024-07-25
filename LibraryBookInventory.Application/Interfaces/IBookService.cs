using LibraryBookInventory.Application.DTOs;

namespace LibraryBookInventory.Application.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<BookDto> GetBookByIdAsync(int id);
        Task AddBookAsync(BookDto book);
        Task UpdateBookAsync(BookDto book);
        Task DeleteBookAsync(int id);
        Task IssueBookAsync(IssueBookDto issueBook);
        Task<IEnumerable<IssuedBookReportDto>> GetIssuedBooksReportAsync();
    }
}
