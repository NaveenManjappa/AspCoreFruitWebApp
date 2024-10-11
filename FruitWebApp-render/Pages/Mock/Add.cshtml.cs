using FruitWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;

namespace FruitWebApp.Pages.Mock
{
    public class AddModel : PageModel
    {
        IHttpClientFactory _httpClientFactory;
        public AddModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [BindProperty]
        public MockFruitModel MockFruit { get; set; }

        public async Task<IActionResult> OnPost()
        {
            var jsonContent=new StringContent(JsonSerializer.Serialize(MockFruit), Encoding.UTF8,
                "application/json");

            var httpClient=this._httpClientFactory.CreateClient("FruitAPI");

            using HttpResponseMessage response = await httpClient.PostAsync("",jsonContent);

            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "Fruit added successfully";
                return RedirectToPage("Index");
            }
            else
            {
                TempData["failure"] = "Failure to add fruit";
                return RedirectToPage("Index");
            }

        }
    }
}
