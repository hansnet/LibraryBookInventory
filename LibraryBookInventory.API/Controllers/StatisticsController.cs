using LibraryBookInventory.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBookInventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public StatisticsController(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        [HttpGet("BookCount")]
        public async Task<ActionResult<int>> GetBookCount()
        {
            var count = await _statisticsRepository.GetBookCountAsync();
            return Ok(count);
        }

        [HttpGet("UserCount")]
        public async Task<ActionResult<int>> GetUserCount()
        {
            var count = await _statisticsRepository.GetUserCountAsync();
            return Ok(count);
        }
    }
}
