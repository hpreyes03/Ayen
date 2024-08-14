using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Ayen.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public List<JokeResult>? DataResult { get; set; }

        public async Task<IActionResult> OnGet()
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync("https://official-joke-api.appspot.com/jokes/ten");

            if (response.IsSuccessStatusCode)
            {
                string? content = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(content))
                {
                    DataResult = JsonConvert.DeserializeObject<List<JokeResult>>(content);
                }
            }
            return Page();
        }
    }

    public class JokeResult
    {
        public string Setup { get; set; }
        public string Punchline { get; set; }
    }
}