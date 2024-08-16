using Microsoft.AspNetCore.Mvc.Rendering;

namespace PokeGame_MVC.Models
{
    public class EnviarRetoViewModel
    {
        public int RetadoId { get; set; }
        public int RetadorPokemonId { get; set; }

        public List<SelectListItem> Pokemones { get; set; }
        public List<SelectListItem> Usuarios { get; set; }
    }
}
