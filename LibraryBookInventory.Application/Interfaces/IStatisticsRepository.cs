namespace LibraryBookInventory.Application.Interfaces
{
    public interface IStatisticsRepository
    {
        Task<int> GetBookCountAsync();
        Task<int> GetUserCountAsync();
    }
}
