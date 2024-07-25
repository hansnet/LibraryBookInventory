using LibraryBookInventory.Application.DTOs;
using LibraryBookInventory.Domain.Entities;

namespace LibraryBookInventory.Application.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(int id);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
        Task IssueBookAsync(IssuedBook dto);
        Task<IEnumerable<IssuedBookReportDto>> GetIssuedBooksReportAsync();
    }
}
