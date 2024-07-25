using LibraryBookInventory.WEB.Models;

namespace LibraryBookInventory.WEB.Services
{
    public interface IBookApiService
    {
        Task<IEnumerable<BookViewModel>> GetBooksAsync();
        Task<BookViewModel> GetBookByIdAsync(int id);
        Task<BookViewModel> AddBookAsync(BookViewModel bookViewModel);
        Task UpdateBookAsync(BookViewModel bookViewModel);
        Task DeleteBookAsync(int id);
    }
}
