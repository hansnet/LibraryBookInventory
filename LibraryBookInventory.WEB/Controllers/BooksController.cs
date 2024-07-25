using LibraryBookInventory.WEB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace LibraryBookInventory.WEB.Controllers
{
    public class BooksController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private static string BaseApiUrl = "https://localhost:7079/api";

        public BooksController(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        private async Task<HttpClient> GetClientWithTokenAsync()
        {
            var client = _httpClientFactory.CreateClient("api");
            var token = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "access_token")?.Value;

            if (token != null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }

        public async Task<IActionResult> Index()
        {
            var client = await GetClientWithTokenAsync();
            var response = await client.GetAsync($"{BaseApiUrl}/books");

            if (response.IsSuccessStatusCode)
            {
                var books = await response.Content.ReadFromJsonAsync<IEnumerable<BookViewModel>>();
                return View(books);
            }

            return RedirectToAction("Login", "Account");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookViewModel model)
        {
            if (ModelState.IsValid)
            {

                var client = await GetClientWithTokenAsync();
                var response = await client.PostAsJsonAsync($"{BaseApiUrl}/books", model);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"Failed to create book. API responded with status code: {response.StatusCode} and message: {errorMessage}");
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var client = await GetClientWithTokenAsync();
            var response = await client.GetAsync($"{BaseApiUrl}/books/{id}");

            if (response.IsSuccessStatusCode)
            {
                var book = await response.Content.ReadFromJsonAsync<BookViewModel>();
                return View(book);
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Failed to get book. API responded with status code: {response.StatusCode} and message: {errorMessage}");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BookViewModel model)
        {
            if (ModelState.IsValid)
            {
                var client = await GetClientWithTokenAsync();
                var response = await client.PutAsJsonAsync($"{BaseApiUrl}/books/{model.Id}", model);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, "Failed to edit book.");
            }

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var client = await GetClientWithTokenAsync();
            var response = await client.GetAsync($"{BaseApiUrl}/books/{id}");

            if (response.IsSuccessStatusCode)
            {
                var book = await response.Content.ReadFromJsonAsync<BookViewModel>();
                return View(book);
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Failed to get book details. API responded with status code: {response.StatusCode} and message: {errorMessage}");
            }

            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = await GetClientWithTokenAsync();
            var response = await client.GetAsync($"{BaseApiUrl}/books/{id}");

            if (response.IsSuccessStatusCode)
            {
                var book = await response.Content.ReadFromJsonAsync<BookViewModel>();
                return View(book);
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Failed to get book details. API responded with status code: {response.StatusCode} and message: {errorMessage}");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var client = await GetClientWithTokenAsync();
            var response = await client.DeleteAsync($"{BaseApiUrl}/books/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Failed to get book details. API responded with status code: {response.StatusCode} and message: {errorMessage}");
            }

            return View();
        }

        public async Task<IActionResult> IssuedBooks()
        {
            var client = await GetClientWithTokenAsync();
            var response = await client.GetAsync($"{BaseApiUrl}/books/report");

            if (response.IsSuccessStatusCode)
            {
                var books = await response.Content.ReadFromJsonAsync<IEnumerable<IssuedBookViewModel>>();
                return View(books);
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Failed to get issued books. API responded with status code: {response.StatusCode} and message: {errorMessage}");
            }

            return RedirectToAction("Index", "Books");
        }

        [HttpGet]
        public async Task<IActionResult> Issue()
        {
            var client = await GetClientWithTokenAsync();

            var booksResponse = await client.GetAsync($"{BaseApiUrl}/books");
            var usersResponse = await client.GetAsync($"{BaseApiUrl}/users");

            if (booksResponse.IsSuccessStatusCode && usersResponse.IsSuccessStatusCode)
            {
                var books = await booksResponse.Content.ReadFromJsonAsync<IEnumerable<IssuedBookViewModel>>();
                var users = await usersResponse.Content.ReadFromJsonAsync<IEnumerable<UserViewModel>>();

                var model = new IssueBookFormViewModel
                {
                    Books = books,
                    Users = users
                };

                return View(model);
            }
            else
            {
                if (!booksResponse.IsSuccessStatusCode)
                {
                    var errorMessage = await booksResponse.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"Failed to get books. API responded with status code: {booksResponse.StatusCode} and message: {errorMessage}");
                }

                if (!usersResponse.IsSuccessStatusCode)
                {
                    var errorMessage = await usersResponse.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"Failed to get users. API responded with status code: {usersResponse.StatusCode} and message: {errorMessage}");
                }
            }

            return View(new IssueBookFormViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Issue(IssueBookFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var client = await GetClientWithTokenAsync();
                var response = await client.PostAsJsonAsync($"{BaseApiUrl}/books/issue", model);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("IssuedBooks");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"Failed to issued book. API responded with status code: {response.StatusCode} and message: {errorMessage}");
                }

                ModelState.AddModelError(string.Empty, "Failed to issue book.");
            }

            return View(model);
        }
    }
}