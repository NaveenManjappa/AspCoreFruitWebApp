using FruitWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace FruitWebApp.Pages.Mock
{
    public class EditModel : PageModel
    {
        IHttpClientFactory _httpClientFactory;
        public EditModel(IHttpClientFactory httpClientFactory) => this._httpClientFactory = httpClientFactory;

        [BindProperty]
        public MockFruitModel MockFruit { get; set; }
        
        public async Task OnGet(int id)
        {
            var httpClient = this._httpClientFactory.CreateClient("FruitAPI");

            using HttpResponseMessage response = await httpClient.GetAsync(id.ToString());

            if(response.IsSuccessStatusCode)
            {
                using var contentStream= await response.Content.ReadAsStreamAsync();
                MockFruit = await JsonSerializer.DeserializeAsync<MockFruitModel>(contentStream);
            }
        }

        public async Task<IActionResult> OnPost()
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(MockFruit),Encoding.UTF8,"application/json");

            var httpClient = this._httpClientFactory.CreateClient("FruitAPI");
            using HttpResponseMessage response = await httpClient.PutAsync(MockFruit.id.ToString(), jsonContent);

            if(response.IsSuccessStatusCode)
            {
                TempData["success"] = "Data updated successfully";
                return RedirectToPage("Index");
            }
            else
            {
                TempData["failure"] = "Data not updated";
                return RedirectToPage("Index");
            }
        }
    }
}
