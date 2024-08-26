using Microsoft.AspNetCore.Mvc.Rendering;

namespace PokeGame_MVC.Models
{
    public class EnviarMensajeViewModel
    {

        public string Mensaje { get; set; }
        public List<SelectListItem> ListaUsuarios { get; set; }

    }
}
