using LibraryBookInventory.Application.DTOs;
using LibraryBookInventory.Application.Interfaces;
using LibraryBookInventory.Domain.Entities;
using LibraryBookInventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryBookInventory.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task AddBookAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task IssueBookAsync(IssuedBook dto)
        {
            var issuedBook = new IssuedBook
            {
                BookId = dto.BookId,
                UserId = dto.UserId,
                IssuedDate = dto.IssuedDate,
                ReturnDate = dto.ReturnDate
            };

            _context.IssuedBooks.Add(issuedBook);
            await _context.SaveChangesAsync();

            //return issuedBook;
        }

        public async Task<IEnumerable<IssuedBookReportDto>> GetIssuedBooksReportAsync()
        {
            return await _context.IssuedBooks
            .Include(ib => ib.Book)
            .Include(ib => ib.User)
            .Select(ib => new IssuedBookReportDto
            {
                Id = ib.BookId,
                Title = ib.Book.Title,
                UserId = ib.UserId,
                UserName = ib.User.UserName,
                IssuedDate = ib.IssuedDate,
                ReturnDate = ib.ReturnDate
            }).ToListAsync();
        }
    }
}
