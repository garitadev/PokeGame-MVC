using Microsoft.AspNetCore.Mvc;
using PokeGame_MVC.DAL;
using PokeGame_MVC.Entities;
using PokeGame_MVC.Models;
using System.Collections.Generic;
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
            try
            {
                DalPokedex dal = new(_httpClient);

                var t = await dal.GetFirst200PokemonsAsync();


               // var response = await _httpClient.GetAsync("https://pokeapi.co/api/v2/pokemon?limit=100");
                //response.EnsureSuccessStatusCode();
                string json = "[{\"name\":\"bulbasaur\",\"url\":\"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/1.png\"},{\"name\":\"ivysaur\",\"url\":\"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/2.png\"},{\"name\":\"venusaur\",\"url\":\"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/3.png\"},{\"name\":\"charmander\",\"url\":\"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/4.png\"},{\"name\":\"charmeleon\",\"url\":\"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/5.png\"},{\"name\":\"charizard\",\"url\":\"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/6.png\"}]";

                //List<Pokemon> pokemons = JsonSerializer.Deserialize<List<Pokemon>>(json);
                List<Pokemon> pokemons = JsonSerializer.Deserialize<List<Pokemon>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });



                return View(t);
            }
            catch (Exception ex)
            {

                throw;
            }
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
