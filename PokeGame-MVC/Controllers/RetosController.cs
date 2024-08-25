using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PokeGame_MVC.Database;
using PokeGame_MVC.Database.PokeGame;
using PokeGame_MVC.Models;
using PokeGame_MVC.Models.Pokedex;
using System.Security.Claims;
using PokeGame_MVC.Helpers;
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
             .Select(r => new RetoModel
             {
                 RetoId = r.RetoId,
                 Retador = r.Retador,
                 FechaEnvio = r.FechaEnvio,
                 PokemonesDipobibles = DbContext.Equipos
                     .Where(e => e.UsuarioId == usuarioId)
                     .Where(e => !DbContext.Enfermeria.Any(enf => enf.PokemonId == e.PokedexId && enf.PokemonId == usuarioId && enf.FechaSalida == null)) // Excluir Pokémon en la enfermería
                     .Select(e => new PokedexModel
                     {
                         Id = e.Pokedex.IdPokemon,
                         Name = e.Pokedex.Nombre,
                         Weight = e.Pokedex.Peso
                     })
                     .ToList()
             })
             .ToList();

            ViewBag.ListaRestos = retos;
            return View(retos);
        }


        [HttpPost]
        public IActionResult ResponderReto(int retoId, int PokemonId, string respuesta)
        {
            PokeGame_MVC.Helpers.Helpers help = new();

            var reto = DbContext.Retos.FirstOrDefault(r => r.RetoId == retoId);

            if (reto == null || reto.RetadoId != Helpers.Helpers.GetCurrentUserId(User))
            {
                return NotFound();
            }

            if (respuesta == "Aceptado")
            {
                reto.RetadoPokemonId = PokemonId;
                reto.Estado = "Aceptado";
                reto.FechaRespuesta = DateTime.Now;

                // Determinar el ganador
                var retadorPokemon = DbContext.Pokedex.FirstOrDefault(p => p.IdPokemon == reto.RetadorPokemonId);
                var retadoPokemon = DbContext.Pokedex.FirstOrDefault(p => p.IdPokemon == reto.RetadoPokemonId);

                if (retadorPokemon != null && retadoPokemon != null)
                {
                    if (retadorPokemon.Peso > retadoPokemon.Peso)
                    {
                        reto.Estado = "Retador Ganó";
                        help.EnviarAPokemonaEnfermeria((int)reto.RetadorPokemonId, reto.RetadorId);

                    }
                    else if (retadorPokemon.Peso < retadoPokemon.Peso)
                    {
                        reto.Estado = "Retado Ganó";
                        help.EnviarAPokemonaEnfermeria((int)reto.RetadoPokemonId, reto.RetadorId);
                    }
                    else
                    {
                        reto.Estado = "Empate";
                    }
                }
            }
            else
            {
                reto.Estado = "Rechazado";
            }

            DbContext.Retos.Update(reto);
            DbContext.SaveChanges();

            return RedirectToAction("MisRetos");
        }


    }
}
