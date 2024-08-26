using PokeGame_MVC.Database.PokeGame;

namespace PokeGame_MVC.Models
{
    public class MensajesModel
    {
        public int MensajeId { get; set; }
        public int RemitenteId { get; set; }
        public int DestinatarioId { get; set; }
        public string Contenido { get; set; }
        public DateTime FechaEnvio { get; set; }

        public Usuario Remitente { get; set; }
        public Usuario Destinatario { get; set; }
    }
}
