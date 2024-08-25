using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var entradaEnfermeria = await DbContext.Enfermeria.FirstOrDefaultAsync(e => e.Id == id);

            if (entradaEnfermeria == null)
            {
                return NotFound();
            }

            entradaEnfermeria.FechaSalida = DateTime.Now;
            DbContext.Enfermeria.Update(entradaEnfermeria);
            await DbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Enfermeria));
        }
    }
}
