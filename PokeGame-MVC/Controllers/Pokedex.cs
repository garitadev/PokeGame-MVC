using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokeGame_MVC.Database;
using PokeGame_MVC.Entities;
using PokeGame_MVC.Models.Pokedex;
using System.Security.Cryptography;

namespace PokeGame_MVC.Controllers
{
    public class Pokedex : Controller
    {

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

        [HttpPost]
        public IActionResult AgregarPokemon([FromBody] PokedexModel pokemon)
        {
            if (pokemon is not null && pokemon?.Id != 0)
            {
                PokeGame_MVC.Database.PokeGame.Pokedex pokemonNew = new PokeGame_MVC.Database.PokeGame.Pokedex()
                {
                    IdPokemon = pokemon.Id,
                    Nombre = pokemon.Name,
                    Peso = pokemon.Weight,
                    Tipo = pokemon.Types.ToString(),
                    Imagen = $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{pokemon.Id}.png",
                    Evoluciones = "No indica",
                    Debilidad = "No indica",
                };

                DbContext.Pokedex.Add(pokemonNew);
                DbContext.SaveChanges();
            }


            return Ok();
        }


        [HttpGet]
        public IActionResult PokedexRead()
        {
            var pokemones = DbContext.Pokedex.Select(t => new PokedexModel
            {
                Id = t.Id,
                Name = t.Nombre,
                Weight = t.Peso,
                Types = t.Tipo,
                Imagen = t.Imagen,
            }).ToList();

            var t = pokemones;

            return View(pokemones);
        }


        [HttpGet]
        [Route("/Pokedex/Editar/{id}")]
        public IActionResult Editar(int id, bool? edit = false)
        {
            if (edit == true)
            {
                TempData["EditSuccess"] = true;

            }

            // var id = 1;
            var pokemon = DbContext.Pokedex.Where(t => t.Id == id).Select(t => new PokedexModel
            {
                Id = t.Id,
                Name = t.Nombre,
                Weight = t.Peso,
                Types = t.Tipo,
                Imagen = t.Imagen,
            }).FirstOrDefault();


            return View(pokemon);
        }

        [HttpPost]
        public ActionResult EditarN(PokedexModel model)
        {

            // Lógica para editar el Pokemon
            var pokemon = DbContext.Pokedex.Find(model.Id);
            if (pokemon == null)
            {
                return NotFound();
            }

            // Actualizar las propiedades del Pokémon
            pokemon.Nombre = model.Name;
            pokemon.Peso = model.Weight;
            pokemon.Tipo = model.Types;
            pokemon.Imagen = model.Imagen;

            DbContext.Update(pokemon);
            DbContext.SaveChanges();

            return RedirectToAction("Editar", new { id = model.Id, edit = true});

        }

    }




}
