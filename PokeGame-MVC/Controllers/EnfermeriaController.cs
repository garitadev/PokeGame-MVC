using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokeGame_MVC.Database.PokeGame;
using PokeGame_MVC.Models;

namespace PokeGame_MVC.Controllers
{
    public class EnfermeriaController : Controller
    {
        private Database.PokeGameContext _dbContext;

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

        // Acción para listar los Pokémon en la enfermería
        public async Task<IActionResult> Enfermeria()
        {
            var pokemonesEnfermeria = await DbContext.Enfermeria
                .Include(e => e.Pokemon)
                .Include(e => e.UsuarioSolicita)
                .Include(e => e.UsuarioAtiende)
                .Where(e => e.FechaSalida == null)
                .Select(e => new EnfermeriaModel
                {
                    EnfermeriaId = e.Id,
                    PokedexId = e.PokemonId,
                    UsuarioId = e.Id,
                    UsuarioAtiendeId = e.UsuarioAtiendeId,
                    FechaIngreso = e.FechaIngreso ,
                    FechaSalida = e.FechaSalida,
                    Pokedex = e.Pokemon,
                    Usuario = e.UsuarioSolicita,
                    UsuarioAtiende = e.UsuarioAtiende
                })
                .ToListAsync();

            return View(pokemonesEnfermeria);
        }

        // Acción para curar un Pokémon y darle de alta
        public async Task<IActionResult> DarDeAlta(int id)
        {
           /* var entradaEnfermeria = await DbContext.Enfermeria
                .Include(e => e.UsuarioSolicita)
                .Include(e => e.Pokemon) // Incluye el Pokedex para acceder a PokedexId
                .FirstOrDefaultAsync(e => e.Id == id);
*/

            var entradaEnfermeria = await DbContext.Enfermeria.FirstOrDefaultAsync(e => e.Id == id);

            var usuarioSolicita = DbContext.Enfermeria.Where(t => t.Id == id)
                .Include(e => e.UsuarioSolicita)
                .FirstOrDefault();
            var pokemonEquipo = DbContext.Equipos.Where(t => t.PokedexId == entradaEnfermeria.PokemonId && t.UsuarioId == usuarioSolicita.UsuarioSolicitaId)
                .FirstOrDefault();

            pokemonEquipo.State = "1";
            if (entradaEnfermeria == null)
            {
                return NotFound();
            }
            
            entradaEnfermeria.FechaSalida = DateTime.Now;
            DbContext.Enfermeria.Update(entradaEnfermeria);
            await DbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Enfermeria));
        }

        public async Task<IActionResult> EnviarAPokemonaEnfermeria(int pokemonId, int usuarioId)
        {
            try
            {
                var random = new Random();
                int count = DbContext.Usuario.Count(t => t.RolId == 3);
                int index = random.Next(count);
                var usuarioAleatorio = DbContext.Usuario
                .Where(u => u.RolId == 3)
                .Skip(index)
                .FirstOrDefault();
                var entradaEnfermeria = new Enfermeria
                {
                    PokemonId = pokemonId,
                    UsuarioAtiendeId = usuarioAleatorio.Id,
                    UsuarioSolicitaId = usuarioId,
                    FechaIngreso = DateTime.Now
                };

                DbContext.Enfermeria.Add(entradaEnfermeria);
                DbContext.SaveChanges();
                var pokemon = DbContext.Equipos.Where(t => t.PokedexId == pokemonId && t.UsuarioId == usuarioId).FirstOrDefault();
                pokemon.State = "3";

                DbContext.SaveChanges();

                return Ok();
            }
            catch (Exception exz)
            {
                return BadRequest();
                throw;
            }

        }
    }
}
