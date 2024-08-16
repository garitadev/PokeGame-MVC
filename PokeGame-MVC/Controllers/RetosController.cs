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
    public class RetosController : Controller
    {

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

        public IActionResult EnviarReto()
        {
            var usuarioId = Helpers.Helpers.GetCurrentUserId(User);

            var pokemones = DbContext.Equipos
                .Where(e => e.UsuarioId == usuarioId)
                .Select(e => new SelectListItem
                {
                    Value = e.Pokedex.Id.ToString(),
                    Text = e.Pokedex.Nombre
                })
                .ToList();

            var usuarios = DbContext.Usuario
                .Where(u => u.Id != usuarioId)
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.Username
                })
                .ToList();

            var model = new EnviarRetoViewModel
            {
                Pokemones = pokemones,
                Usuarios = usuarios
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult EnviarReto(int retadoId, int retadorPokemonId)
        {
            var retadorId = Helpers.Helpers.GetCurrentUserId(User);

            var reto = new Retos
            {
                RetadorId = retadorId,
                RetadoId = retadoId,
                RetadorPokemonId = retadorPokemonId,
                Estado = "Pendiente",
                FechaEnvio = DateTime.Now
            };

            DbContext.Retos.Add(reto);
            DbContext.SaveChanges();

            return RedirectToAction("MisRetos");
        }

        [HttpGet]
        public IActionResult MisRetos()
        {
            var usuarioId = Helpers.Helpers.GetCurrentUserId(User);
            var retos = DbContext.Retos
                .Where(r => r.RetadoId == usuarioId && r.Estado == "Pendiente")
                .Include(r => r.Retador)
                .Include(r => r.RetadorPokemon)
                .Select(e => new RetoModel
                {
                    Retado = e.Retado,
                    Retador = e.Retador,
                    FechaEnvio = e.FechaEnvio,
                }).ToList();

            ViewBag.ListaRestos = retos;
            return View(retos);
        }

    }
}
