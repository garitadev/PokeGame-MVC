using Microsoft.EntityFrameworkCore;
using PokeGame_MVC.Database.PokeGame;
using System.Security.Claims;
using System.Security.Policy;

namespace PokeGame_MVC.Helpers
{
    public class Helpers
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
        public static int GetCurrentUserId(ClaimsPrincipal User)
        {
            return int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
        }


        public void EnviarAPokemonaEnfermeria(int pokemon, int usuarioId)
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
                    PokemonId = pokemon,
                    UsuarioAtiendeId = usuarioAleatorio.Id,
                    UsuarioSolicitaId = usuarioId,
                    FechaIngreso = DateTime.Now
                };

                DbContext.Enfermeria.Add(entradaEnfermeria);
                DbContext.SaveChanges();
            }
            catch (Exception exz)
            {

                throw;
            }
           
        }
    }
}
