using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PokeGame_MVC.Database.PokeGame;
using System.Security.Claims;
using PokeGame_MVC.Models;

namespace PokeGame_MVC.Controllers
{
    public class MensajesController : Controller
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

        // Acción para mostrar la bandeja de entrada del usuario
        public IActionResult BandejaDeEntrada()
        {
            var usuarioId = GetCurrentUserId();
            var mensajes = DbContext.Mensajes
                .Where(m => m.DestinatarioId == usuarioId)
                .Include(m => m.Remitente)
                .OrderByDescending(m => m.FechaEnvio)
                .Select(m => new MensajesModel
                {
                    MensajeId = m.Id,
                    Remitente = m.Remitente,
                    Contenido = m.Contenido,
                    FechaEnvio = m.FechaEnvio
                })
                .ToList();

            return View(mensajes);
        }

        // Acción para mostrar el formulario de envío de un nuevo mensaje
        public IActionResult EnviarMensaje()
        {
            // Verificamos si el contexto está bien configurado y contiene datos
            var usuarios = DbContext.Usuario.ToList(); // Asegúrate de que la tabla es Usuarios

            if (usuarios == null || !usuarios.Any())
            {
                // Puedes manejar el caso cuando no hay usuarios disponibles
                // Por ejemplo, mostrar un mensaje de error en la vista
                ViewBag.ErrorMessage = "No hay usuarios disponibles para enviar un mensaje.";
                return View();
            }
            var listaUsuarios = usuarios.Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.Username
            }).ToList();

            var model = new EnviarMensajeViewModel
            {
                ListaUsuarios = listaUsuarios
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult EnviarMensaje(int destinatarioId, string contenido)
        {
            var usuarioId = GetCurrentUserId();

            var mensaje = new Mensajes
            {
                RemitenteId = usuarioId,
                DestinatarioId = destinatarioId,
                Contenido = contenido,
                FechaEnvio = DateTime.Now
            };

            DbContext.Mensajes.Add(mensaje);
            DbContext.SaveChanges();

            return RedirectToAction("BandejaDeEntrada");
        }

        // Nueva acción para responder a un mensaje
        public IActionResult ResponderMensaje(int mensajeId)
        {
            var mensajeOriginal = DbContext.Mensajes
                .Include(m => m.Remitente)
                .FirstOrDefault(m => m.Id == mensajeId);

            if (mensajeOriginal == null || mensajeOriginal.DestinatarioId != GetCurrentUserId())
            {
                return NotFound();
            }

            var respuestaViewModel = new RespuestaMensajeViewModel
            {
                MensajeId = mensajeOriginal.Id,
                DestinatarioId = mensajeOriginal.RemitenteId,
                RemitenteNombre = mensajeOriginal.Remitente.Nombre
            };

            return View(respuestaViewModel);
        }

        [HttpPost]
        public IActionResult ResponderMensaje(RespuestaMensajeViewModel model)
        {
            model.RemitenteNombre = "Prueba";
            if (!ModelState.IsValid)
            {
                var usuarioId = GetCurrentUserId();

                var mensaje = new Mensajes
                {
                    RemitenteId = usuarioId,
                    DestinatarioId = model.DestinatarioId,
                    Contenido = model.Contenido,
                    FechaEnvio = DateTime.Now
                };

                DbContext.Mensajes.Add(mensaje);
                DbContext.SaveChanges();

                return RedirectToAction("BandejaDeEntrada");
            }

            return View(model);
        }

        public  IActionResult MensajesEnviados()
        {
            var usuarioId = Helpers.Helpers.GetCurrentUserId(User);

            var mensajesEnviados =  DbContext.Mensajes
                .Include(m => m.Destinatario)
                .Where(m => m.RemitenteId == usuarioId)
                .Select(m => new MensajesModel
                {
                    FechaEnvio = m.FechaEnvio,
                    Destinatario = m.Destinatario,
                    Contenido = m.Contenido,
                })
                .ToList();

            return View(mensajesEnviados);
        }

        // Método para obtener el ID del usuario actual
        private int GetCurrentUserId()
        {
            return int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
        }
    }
}
