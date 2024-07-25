using LibraryBookInventory.Application.DTOs;
using LibraryBookInventory.Application.Interfaces;
using LibraryBookInventory.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBookInventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBook(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] BookDto bookDto)
        {
            await _bookService.AddBookAsync(bookDto);
            return CreatedAtAction(nameof(GetBook), new { id = bookDto.Id }, bookDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookDto bookDto)
        {
            if (id != bookDto.Id)
            {
                return BadRequest();
            }
            await _bookService.UpdateBookAsync(bookDto);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("issue")]
        public async Task<IActionResult> IssueBook([FromBody] IssueBookDto dto)
        {
            await _bookService.IssueBookAsync(dto);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("report")]
        public async Task<IActionResult> GetIssuedBooksReport()
        {
            var report = await _bookService.GetIssuedBooksReportAsync();
            return Ok(report);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return NoContent();
        }
    }
}
