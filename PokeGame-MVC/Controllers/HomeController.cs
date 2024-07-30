using Microsoft.AspNetCore.Mvc;
using PokeGame_MVC.Entities;
using PokeGame_MVC.Models;
using System.Diagnostics;
using System.Text.Json;

namespace PokeGame_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;

        

        public HomeController(ILogger<HomeController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;

        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("https://pokeapi.co/api/v2/pokemon?limit=100");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var pokemons = JsonSerializer.Deserialize<PokeApiResponse>(json);

            var res =  pokemons.Results.Select(p => new Pokemon
            {
                Id = int.Parse(p.Url.Split('/').Reverse().Skip(1).First()),
                Name = p.Name,
                ImageUrl = $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{1}.png"
            }).ToList();
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
}
