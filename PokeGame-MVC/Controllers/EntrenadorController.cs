using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PokeGame_MVC.Controllers
{
    public class EntrenadorController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;



        public EntrenadorController(ILogger<HomeController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;

        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AgregarPokemon(int PokemonId)
        {
            var usuarioId = GetCurrentUserId();

            // Verifica si ya tiene el Pokémon en su equipo
            var yaEnEquipo = _context.Equipos.Any(e => e.UsuarioId == usuarioId && e.PokemonId == PokemonId);
            if (yaEnEquipo)
            {
                ModelState.AddModelError("", "Ya tienes este Pokémon en tu equipo.");
                return View(); // Regresa a la vista con el error
            }

            

            var equipo = new Equipo
            {
                UsuarioId = usuarioId,
                PokemonId = PokemonId
            };

            _context.Equipos.Add(equipo);
            _context.SaveChanges();

            return RedirectToAction("MiEquipo");
        }
        public IActionResult MiEquipo()
        {

            return View();
        }
    }
}
