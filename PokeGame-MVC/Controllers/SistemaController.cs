using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using PokeGame_MVC.Database;
using PokeGame_MVC.Database.PokeGame;
using PokeGame_MVC.Models;
using System.Data;

namespace PokeGame_MVC.Controllers
{
    public class SistemaController : Controller
    {
        private PasswordHasher<object> passwordHasher = new PasswordHasher<object>();

        private PokeGameContext _dbContext;

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

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Ingresar()
        {

            return View();
        }
        [HttpPost]
        //[Route("/Ingresar")]
        public ActionResult Ingresar(UsuarioModel usuario)
        {
            var usuarioExiste = DbContext.Usuario.Where(t => t.Email == usuario.Email).Select(t => t.PasswordHash).FirstOrDefault();

            if (usuarioExiste != null)
            {
                var result = passwordHasher.VerifyHashedPassword(null, usuarioExiste, usuario.PasswordHash);
                result = PasswordVerificationResult.Success;

                if (result.Equals("Success"));
                {
                    return RedirectToAction("Index", "Home");
                }
               
            }
            
            return View();
        }

        [HttpGet]
        public IActionResult Registrar()
        {

            return View();
        }
        [HttpPost]
        //[Route("/Ingresar")]
        public ActionResult Registrar(UsuarioModel usuario)
        {
            //Validar que el usuario no extista
            var usuarioExisten = DbContext.Usuario.Where(t => t.Username == usuario.Username).Select(t => t.Username).FirstOrDefault();
            var existeEmail= DbContext.Usuario.Where(t => t.Email == usuario.Email).Select(t => t.Email).FirstOrDefault();
            if (usuarioExisten != null)
            {
                ViewBag.Error = "El usuario ingresado ya existe";
                return View();
            }
            if (existeEmail != null)
            {
                ViewBag.Error = "El email ingresado ya existe";
                return View();
            }
            if (usuario.PasswordHash != usuario.PasswordVerify)
            {
                ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }
            var hashedPassword = passwordHasher.HashPassword(usuario.Email, usuario.PasswordHash);
            Usuario userNew = new Usuario()
            {
                Nombre = "Test",
                    Username = usuario.Username,
                PasswordHash = hashedPassword,
                Email = usuario.Email,
                FechaCreacion = DateTime.Now,
                RolId = 2 // Rol default de entrenador para neubso usaurios
            };

            DbContext.Usuario.Add(userNew);
            DbContext.SaveChanges();

            return RedirectToAction("Ingresar");
        }

        [HttpGet]
        [Route("/Sistemas/Editar/{id}")]

        public IActionResult Editar(int id, bool? edit = false)
        {
            if (edit == true)
            {
                TempData["EditSuccess"] = true;
            }

            var usuario = DbContext.Usuario.Where(t => t.Id == id).Select(t => new UsuarioModel
            {
                RolId = t.RolId,
                Email = t.Email,
                Username = t.Username,
                Nombre = t.Nombre,
                FechaCreacion = t.FechaCreacion
            }).FirstOrDefault();

            return View(usuario);
        }
        [HttpPost]
        public IActionResult EditarUsuario(UsuarioModel model)
        {
            var usuario = DbContext.Usuario.Find(model.Id);
            if (usuario == null)
            {
                return NotFound();
            }

            usuario.Nombre = model.Nombre ?? usuario.Nombre;
            usuario.Username = model.Username ?? usuario.Username;
            usuario.Email = model.Email ?? usuario.Email;
            usuario.RolId = usuario.RolId;

            DbContext.Update(usuario);
            DbContext.SaveChanges();

            return RedirectToAction("Editar", new { id = model.Id, edit = true });
        }



        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            var usuario = DbContext.Usuario.Find(id);

            if (usuario != null)
            {
                DbContext.Usuario.Remove(usuario);
                DbContext.SaveChangesAsync();
            }
            return Ok();
        }


        [HttpGet]
        public IActionResult Listar()
        {
            
            var usuarios = DbContext.Usuario.Select(t => new UsuarioModel
            {
                Id = t.Id,
                RolId = t.RolId,
                Email = t.Email,
                FechaCreacion = t.FechaCreacion,
                Nombre = t.Nombre,
                Username = t.Username,
            }).ToList();


            return View(usuarios);
        }
    }
}
