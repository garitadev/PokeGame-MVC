﻿using Microsoft.AspNetCore.Identity;
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

        public IActionResult Editar(UsuarioModel usuario)
        {


            return View();
        }


        public IActionResult Delete(int id) {

            return Ok();
        }


        [HttpGet]
        public IActionResult Listar()
        {
            
            var usuarios = DbContext.Usuario.Select(t => new UsuarioModel
            {
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
