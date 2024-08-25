using PokeGame_MVC.Database.PokeGame;
using PokeGame_MVC.Entities;
using PokeGame_MVC.Models.Pokedex;

namespace PokeGame_MVC.Models
{
    public class RetoModel
    {
        public int RetoId { get; set; }
        public int RetadorId { get; set; }
        public int RetadoId { get; set; }
        public string Estado { get; set; } // Pendiente, Aceptado, Rechazado
        public DateTime FechaEnvio { get; set; }
        public DateTime? FechaRespuesta { get; set; }

        public int? RetadorPokemonId { get; set; }
        public int? RetadoPokemonId { get; set; }

        public Usuario Retador { get; set; }
        public Usuario Retado { get; set; }
        public Pokemon RetadorPokemon { get; set; }
        public Pokemon RetadoPokemon { get; set; }

        public List<PokedexModel> PokemonesDipobibles { get; set; }
    }
}
