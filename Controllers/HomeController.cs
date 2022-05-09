using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using postpage.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Data;

namespace postpage.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    string baseUrl = "https://jsonplaceholder.typicode.com/";

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index(){
        DataTable dt = new DataTable();
        using(var client = new HttpClient()){
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage getData= await client.GetAsync("photos");
            if(getData.IsSuccessStatusCode){
                string result = getData.Content.ReadAsStringAsync().Result;
                dt = JsonConvert.DeserializeObject<DataTable>(result);
                
            } else{
                Console.WriteLine("Data not found");
            }
            ViewData.Model = dt;
        }
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
