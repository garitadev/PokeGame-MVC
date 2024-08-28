using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PokeGame_MVC.Database;
using PokeGame_MVC.Database.PokeGame;
using PokeGame_MVC.Models;
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
                PokedexId = id,
                State = "1"

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
            .Include(e => e.Pokedex)
            .Select(e => new PokedexModel
            {
                Name = e.Pokedex.Nombre,
                Id = e.Pokedex.Id,
                Weight = e.Pokedex.Peso,
                Types = e.Pokedex.Tipo,
                Imagen = e.Pokedex.Imagen,
                State = e.State,
            })
            .ToList();


            ViewBag.UsuarioId = usuarioId;
            return View(equipo);
        }

        public async Task<IActionResult> HistorialRetos()
        {
            var usuarioId = Helpers.Helpers.GetCurrentUserId(User);

            var retos = await DbContext.Retos
                .Where(r => r.RetadorId == usuarioId || r.RetadoId == usuarioId)
                .OrderByDescending(r => r.FechaEnvio)
                .Select(r => new RetoModel
                {
                    RetoId = r.RetoId,
                    RetadorId = r.RetadorId,
                    RetadoId = r.RetadoId,
                    Estado = r.Estado,
                    FechaEnvio = r.FechaEnvio,
                    FechaRespuesta = r.FechaRespuesta,
                    RetadorPokemonId = r.RetadorPokemonId,
                    RetadoPokemonId = r.RetadoPokemonId,

                    Retador = r.Retador,
                    Retado = r.Retado,
                    //RetadorPokemon = r.RetadoPokemon.Nombre,
                    //RetadoPokemon = r.RetadoPokemon,

                })
                .ToListAsync();

            return View(retos);
        }

        private int GetCurrentUserId()
        {
            return int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
        }
    }
}
