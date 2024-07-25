using LibraryBookInventory.Application.DTOs;
using LibraryBookInventory.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryBookInventory.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<IssuedBook> IssuedBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Book>()
                .HasIndex(c => c.ISBN)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }

        public async Task<int> IssueBookAsync(int bookId, string userId, DateTime issuedDate)
        {
            var result = await Database.ExecuteSqlRawAsync("EXEC IssueBook @p0, @p1, @p2", bookId, userId, issuedDate);
            return result;
        }

        public async Task<List<IssuedBookReportDto>> GetIssuedBooksReportAsync()
        {
            return await IssuedBooks
                .FromSqlRaw("EXEC GetIssuedBooksReport")
                .Select(ib => new IssuedBookReportDto
                {
                    Id = ib.BookId,
                    Title = ib.Book.Title,
                    UserId = ib.UserId,
                    UserName = ib.User.UserName,
                    IssuedDate = ib.IssuedDate,
                    ReturnDate = ib.ReturnDate
                })
                .ToListAsync();
        }
    }
}
