using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Frontend.Pages
{
    public class ErrorModel : PageModel
    {
        public string? ErrorMessage { get; set; }

        public void OnGet(string message)
        {
            ErrorMessage = message ?? "An unknown error occurred.";
        }
    }
}
