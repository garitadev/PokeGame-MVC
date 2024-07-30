using System.ComponentModel.DataAnnotations;

namespace PokeGame_MVC.Entities
{
    public class UsuarioModel
    {
        public int RolId { get; set; }
        public string Nombre { get; set; }
        //[Required(ErrorMessage = "La contraseña es requerida")]
        public string PasswordHash { get; set; }
        public string PasswordVerify { get; set; }

        //[Required(ErrorMessage = "El correo es requerido")]
        public string Email { get; set; }

        public string Username { get; set; }

        public string FotoPerfil { get; set; }
        public DateTime FechaCreacion { get; set; }

    }
}
