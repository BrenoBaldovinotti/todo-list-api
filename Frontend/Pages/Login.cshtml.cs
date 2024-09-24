using _Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace _Frontend.Pages
{
    public class LoginModel(HttpClient httpClient) : PageModel
    {
        private readonly HttpClient _httpClient = httpClient;

        [BindProperty]
        public required string Username { get; set; }

        [BindProperty]
        public required string Password { get; set; }

        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                var loginData = new
                {
                    this.Username,
                    this.Password
                };

                var jsonContent = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("http://localhost:5224/api/v1/users/login", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var loginResponse = JsonSerializer.Deserialize<ApiResponseModel<LoginData>>(jsonResponse);

                    if (loginResponse?.Status == "success")
                    {
                        var token = loginResponse?.Data?.Token;

                        if (token is not null)
                        {
                            HttpContext.Session.SetString("Token", token);

                            return RedirectToPage("/Index");
                        }

                        return Page();

                    }
                    else
                    {
                        ErrorMessage = loginResponse?.Message;
                    }
                }
                else
                {
                    return RedirectToPage("/Error", new { message = "Login failed. Please try again." });
                }

                return Page();
            }
            catch (Exception ex)
            {
                return RedirectToPage("/Error", new { message = "An unexpected error occurred: " + ex.Message });
                throw;
            }
            
        }
    }

    public class LoginData
    {
        [JsonPropertyName("token")]
        public string? Token { get; set; }
    }
}
