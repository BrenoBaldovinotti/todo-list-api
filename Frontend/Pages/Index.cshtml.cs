using _Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;  
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace _Frontend.Pages
{
    public class IndexModel(HttpClient httpClient) : PageModel
    {
        [BindProperty]
        public required string NewIssueName { get; set; }
        public IEnumerable<IssueDto> Issues { get; set; } = [];
        public string? ErrorMessage { get; set; }

        public async Task OnGet()
        {
            await LoadIssues();
        }

        public async Task<IActionResult> OnPostAddIssue()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var token = HttpContext.Session.GetString("Token");

            if (string.IsNullOrEmpty(token))
            {
                ErrorMessage = "Unauthorized. Please login again.";
                return RedirectToPage("/Login");
            }

            var newIssue = new { Name = NewIssueName };

            var jsonContent = new StringContent(JsonSerializer.Serialize(newIssue), Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.PostAsync("http://localhost:5224/api/v1/issues", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ApiResponseModel<object>>(jsonResponse);

                if (apiResponse?.Status == "success")
                {
                    return RedirectToPage("/Index");
                }
                else
                {
                    ErrorMessage = apiResponse?.Message;
                }
            }

            ErrorMessage = "Failed to add issue.";
            return Page();
        }

        public async Task<IActionResult> OnPostCompleteIssue(Guid id)
        {
            var token = HttpContext.Session.GetString("Token");

            if (string.IsNullOrEmpty(token))
            {
                ErrorMessage = "Unauthorized. Please login again.";
                return RedirectToPage("/Login");
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.PatchAsync($"http://localhost:5224/api/v1/issues/{id}/complete", null);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ApiResponseModel<object>>(jsonResponse);

                if (apiResponse?.Status == "success")
                {
                    return RedirectToPage("/Index");
                }
                else
                {
                    ErrorMessage = apiResponse?.Message;
                }
            }

            ErrorMessage = "Failed to complete issue.";
            return Page();
        }

        public async Task<IActionResult> OnPostRemoveCompleted()
        {
            var token = HttpContext.Session.GetString("Token");

            if (string.IsNullOrEmpty(token))
            {
                ErrorMessage = "Unauthorized. Please login again.";
                return RedirectToPage("/Login");
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.DeleteAsync("http://localhost:5224/api/v1/issues/completed");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ApiResponseModel<object>>(jsonResponse);

                if (apiResponse?.Status == "success")
                {
                    return RedirectToPage("/Index");
                }
                else
                {
                    ErrorMessage = apiResponse?.Message;
                }
            }

            ErrorMessage = "Failed to remove completed issues.";
            return Page();
        }

        private async Task LoadIssues()
        {
            var token = HttpContext.Session.GetString("Token");

            if (string.IsNullOrEmpty(token))
            {
                ErrorMessage = "Unauthorized. Please login again.";
                return;
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync("http://localhost:5224/api/v1/issues");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ApiResponseModel<IEnumerable<IssueDto>>>(jsonResponse);

                if (apiResponse?.Status == "success")
                {
                    Issues = apiResponse?.Data;
                }
                else
                {
                    ErrorMessage = apiResponse?.Message;
                }
            }
            else
            {
                ErrorMessage = "Failed to load issues.";
            }
        }

        public class IssueDto
        {
            [JsonPropertyName("id")]
            public Guid Id { get; set; }

            [JsonPropertyName("name")]
            public required string Name { get; set; }

            [JsonPropertyName("isCompleted")]
            public bool IsCompleted { get; set; }

            [JsonPropertyName("createdAt")]
            public DateTime CreatedAt { get; set; }
        }
    }
}
