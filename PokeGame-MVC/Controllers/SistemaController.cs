using Microsoft.AspNetCore.Mvc;
using PokeGame_MVC.Entities;

namespace PokeGame_MVC.Controllers
{
    public class SistemaController : Controller
    {
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
        

            return View();  
        }
    }
}
