using Microsoft.AspNetCore.Mvc;

namespace PokeGame_MVC.Controllers
{
    public class Pokedex : Controller
    {


        public IActionResult Index()
        {
            return View();
        }
    }
}
