using LibraryBookInventory.WEB.Models;

namespace LibraryBookInventory.WEB.Services
{
    public class BookApiService : IBookApiService
    {
        private readonly HttpClient _httpClient;

        public BookApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<BookViewModel>> GetBooksAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<BookViewModel>>("api/books");
        }

        public async Task<BookViewModel> GetBookByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<BookViewModel>($"api/books/{id}");
        }

        public async Task<BookViewModel> AddBookAsync(BookViewModel bookViewModel)
        {
            var response = await _httpClient.PostAsJsonAsync("api/books", bookViewModel);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<BookViewModel>();
        }

        public async Task UpdateBookAsync(BookViewModel bookViewModel)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/books/{bookViewModel.Id}", bookViewModel);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteBookAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/books/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
