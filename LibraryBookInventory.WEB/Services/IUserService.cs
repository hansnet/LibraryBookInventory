using LibraryBookInventory.WEB.Models;

namespace LibraryBookInventory.WEB.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();

    }
}
