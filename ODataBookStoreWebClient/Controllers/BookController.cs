using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ODataBookStore.Models;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;

namespace ODataBookStoreWebClient.Controllers
{
    public class BookController : Controller
    {
        private readonly HttpClient client = null;

        private string ProductApiUrl = "";

        public BookController ()
        {
            client = new HttpClient ();

            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "http://localhost:50246/odata/Books";
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            dynamic temp = JObject.Parse(strData);
            var lst = temp.value;

            List<Book> items = ((JArray)temp.value).Select(x => new Book
            {
                Id = (int)x["Id"],
                Author = (string)x["Author"],
                ISBN = (string)x["ISBN"],
                Title = (string)x["Title"],
                Price = (decimal)x["Price"],
            }).ToList();

            return View(items);
        }
    }
}
