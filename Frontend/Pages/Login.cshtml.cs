using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;

namespace _Frontend.Pages
{
    public class LoginModel(HttpClient httpClient) : PageModel
    {
        [BindProperty]
        public required string Username { get; set; }

        [BindProperty]
        public required string Password { get; set; }

        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnPost()
        {
            var loginData = new
            {
                this.Username,
                this.Password
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("http://localhost:5224/api/v1/users/login", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();

                return RedirectToPage("/HelloWorld");
            }
            else
            {
                // Login failed, show error
                ErrorMessage = "Invalid username or password.";
                return Page();
            }
        }
    }
}
