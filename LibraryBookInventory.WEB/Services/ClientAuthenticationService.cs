using LibraryBookInventory.WEB.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace LibraryBookInventory.WEB.Services
{
    public class ClientAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private static string BaseApiUrl = "https://localhost:7079/api/";

        public ClientAuthenticationService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TokenResponse> RegisterAsync(RegisterModel model)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseApiUrl}authentication/register", model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<TokenResponse>();
            }

            throw new Exception("Registration failed: " + await response.Content.ReadAsStringAsync());
        }

        public async Task<TokenResponse> LoginAsync(LoginModel model)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseApiUrl}authentication/login", model);

            if (response.IsSuccessStatusCode)
            {
                var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();
                if (tokenResponse != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.Username),
                        new Claim("access_token", tokenResponse.Token)
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return tokenResponse;
                }
            }

            throw new Exception("Invalid login attempt");
        }

        public async Task LogoutAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
