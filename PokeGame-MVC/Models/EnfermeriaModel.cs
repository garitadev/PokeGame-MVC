using PokeGame_MVC.Database.PokeGame;
using PokeGame_MVC.Entities;

namespace PokeGame_MVC.Models
{
    public class EnfermeriaModel
    {
        public int EnfermeriaId { get; set; }
        public int PokedexId { get; set; }
        public int UsuarioId { get; set; }
        public int UsuarioAtiendeId { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaSalida { get; set; }

        public PokeGame_MVC.Database.PokeGame.Pokedex Pokedex { get; set; }
        public Usuario Usuario { get; set; }
        public Usuario UsuarioAtiende { get; set; }
    }

}
