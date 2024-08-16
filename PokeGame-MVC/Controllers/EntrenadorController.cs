using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokeGame_MVC.Database;
using PokeGame_MVC.Database.PokeGame;
using PokeGame_MVC.Models.Pokedex;
using System.Security.Claims;
namespace PokeGame_MVC.Controllers
{
    public class EntrenadorController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;

        private PokeGameContext _dbContext;

        public Database.PokeGameContext DbContext
        {
            get
            {
                _dbContext ??= new Database.PokeGameContext();
                return _dbContext;
            }
            set
            {
                _dbContext = value;
            }
        }

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
        // [Route("/Entrenador/AgregarPokemon/{id}")]

        public IActionResult AgregarPokemon(int id)
        {
            var usuarioId = GetCurrentUserId();

            // Verifica si ya tiene el Pokémon en su equipo
            var yaEnEquipo = DbContext.Equipos.Any(e => e.UsuarioId == usuarioId && e.PokedexId == id);
            if (yaEnEquipo)
            {
                ModelState.AddModelError("", "Ya tienes este Pokémon en tu equipo.");
                return View(); // Regresa a la vista con el error
            }



            var equipo = new Equipos
            {
                UsuarioId = usuarioId,
                PokedexId = id
            };

            DbContext.Equipos.Add(equipo);
            DbContext.SaveChanges();

            return RedirectToAction("MiEquipo");
        }
        public IActionResult MiEquipo()
        {
            var usuarioId = GetCurrentUserId();
            var equipo = DbContext.Equipos
                .Where(e => e.UsuarioId == usuarioId)
                .Include(e => e.Pokedex) // Esto asume que tienes una relación de navegación en el modelo
                .Select(e => new PokedexModel
                {
                    Name = e.Pokedex.Nombre,
                    Id = e.Pokedex.Id,
                    Weight = e.Pokedex.Peso,
                    Types = e.Pokedex.Tipo,
                    Imagen = e.Pokedex.Imagen
                }).ToList();

            return View(equipo);
        }

        private int GetCurrentUserId()
        {
            return int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
        }
    }
}
