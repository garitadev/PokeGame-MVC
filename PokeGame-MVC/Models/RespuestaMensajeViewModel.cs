namespace PokeGame_MVC.Models
{
    public class RespuestaMensajeViewModel
    {
        public int MensajeId { get; set; }
        public int DestinatarioId { get; set; }
        public string RemitenteNombre { get; set; }
        public string Contenido { get; set; }
    }
}
