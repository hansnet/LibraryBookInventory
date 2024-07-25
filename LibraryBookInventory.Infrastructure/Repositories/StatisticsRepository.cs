using LibraryBookInventory.Application.Interfaces;
using LibraryBookInventory.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LibraryBookInventory.Infrastructure.Repositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly AppDbContext _context;

        public StatisticsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetBookCountAsync()
        {
            var bookCountParam = new SqlParameter("@BookCount", SqlDbType.Int) { Direction = ParameterDirection.Output };
            await _context.Database.ExecuteSqlRawAsync("EXEC GetBookCount @BookCount OUTPUT", bookCountParam);
            return (int)bookCountParam.Value;
        } 

        public async Task<int> GetUserCountAsync()
        {
            var userCountParam = new SqlParameter("@UserCount", SqlDbType.Int) { Direction = ParameterDirection.Output };
            await _context.Database.ExecuteSqlRawAsync("EXEC GetUserCount @UserCount OUTPUT", userCountParam);
            return (int)userCountParam.Value;
        }
    }
}
