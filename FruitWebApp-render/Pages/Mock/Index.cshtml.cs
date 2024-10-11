using FruitWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace FruitWebApp.Pages.Mock
{
    public class IndexModel : PageModel
    {
        IHttpClientFactory _httpClientFactory;
        public IndexModel(IHttpClientFactory httpClientFactory) => this._httpClientFactory = httpClientFactory;

        [BindProperty]
        public IEnumerable<MockFruitModel> MockFruits { get; set; } = default!;
   
        public async Task OnGet()
        {
            var httpClient = this._httpClientFactory.CreateClient("FruitAPI");
            using HttpResponseMessage response = await httpClient.GetAsync("");

            if (response.IsSuccessStatusCode)
            {
               var contentStream= await response.Content.ReadAsStreamAsync();
                MockFruits = await JsonSerializer.DeserializeAsync<IEnumerable<MockFruitModel>>(contentStream);
            }
            
        }
    }
}
