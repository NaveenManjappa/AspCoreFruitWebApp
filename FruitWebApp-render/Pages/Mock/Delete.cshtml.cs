using FruitWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace FruitWebApp.Pages.Mock
{
    public class DeleteModel : PageModel
    {
        IHttpClientFactory _httpClientFactory;
       
        public DeleteModel(IHttpClientFactory httpClientFactory ) => this._httpClientFactory = httpClientFactory;

        [BindProperty]
        public MockFruitModel MockFruit { get; set; }

        public async Task OnGet(int id)
        {
            var httpClient = this._httpClientFactory.CreateClient("FruitAPI");
            using HttpResponseMessage response = await httpClient.GetAsync(id.ToString());

            if(response.IsSuccessStatusCode)
            {
                using var contentStream = await response.Content.ReadAsStreamAsync();
                MockFruit = await JsonSerializer.DeserializeAsync<MockFruitModel>(contentStream);
            }
            
        }

        public async Task<IActionResult> OnPost()
        {
            var httpClient = this._httpClientFactory.CreateClient("FruitAPI");

            using HttpResponseMessage response=await httpClient.DeleteAsync(MockFruit.id.ToString());
            if(response.IsSuccessStatusCode)
            {
                TempData["success"] = "Deleted successfully";
                return RedirectToPage("Index");
            }
            else
            {
                TempData["failure"] = "Unable to delete";
                return RedirectToPage("Index");
            }
        }
    }
}
