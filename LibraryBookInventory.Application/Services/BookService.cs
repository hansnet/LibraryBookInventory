using LibraryBookInventory.Application.DTOs;
using LibraryBookInventory.Application.Interfaces;
using LibraryBookInventory.Domain.Entities;

namespace LibraryBookInventory.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            return books.Select(book => new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                PublishedYear = book.PublishedYear,
                Genre = book.Genre,
            });
        }

        public async Task<BookDto?> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            return book == null ? null : new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                PublishedYear = book.PublishedYear,
                Genre = book.Genre
            };
        }

        public async Task AddBookAsync(BookDto bookDto)
        {
            var book = new Book
            {
                Title = bookDto.Title,
                Author = bookDto.Author,
                ISBN = bookDto.ISBN,
                PublishedYear = bookDto.PublishedYear,
                Genre = bookDto.Genre,
            };
            await _bookRepository.AddBookAsync(book);
        }

        public async Task UpdateBookAsync(BookDto bookDto)
        {
            var book = new Book
            {
                Id = bookDto.Id,
                Title = bookDto.Title,
                Author = bookDto.Author,
                ISBN = bookDto.ISBN,
                PublishedYear = bookDto.PublishedYear,
                Genre = bookDto.Genre,
            };
            await _bookRepository.UpdateBookAsync(book);
        }

        public async Task DeleteBookAsync(int id)
        {
            await _bookRepository.DeleteBookAsync(id);
        }

        public async Task IssueBookAsync(IssueBookDto issueBook)
        {
            var book = new IssuedBook
            {
                BookId = issueBook.BookId,
                UserId = issueBook.UserId,
                IssuedDate = issueBook.IssuedDate,
                ReturnDate = issueBook.ReturnDate,
            };
            await _bookRepository.IssueBookAsync(book);
        }

        public async Task<IEnumerable<IssuedBookReportDto>> GetIssuedBooksReportAsync()
        {
            var report = await _bookRepository.GetIssuedBooksReportAsync();
            return report;
        }

        //public async Task IssueBookAsync(IssueBookDto dto)
        //{
        //    await _dbContext.IssueBookAsync(dto.BookId, dto.UserId, dto.IssuedDate);
        //}

        //public async Task<IEnumerable<IssuedBookReportDto>> GetIssuedBooksReportAsync()
        //{
        //    return await _dbContext.GetIssuedBooksReportAsync();
        //}
    }
}
