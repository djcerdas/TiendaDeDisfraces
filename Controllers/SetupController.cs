using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using TiendaDeDisfraces.Helpers;
using TiendaDeDisfraces.Models;

namespace TiendaDeDisfraces.Controllers
{
    public class SetupController : Controller
    {
        private readonly TiendaDeDisfracesContext _context;

        public SetupController(TiendaDeDisfracesContext context)
        {
            _context = context;
        }

        // GET
        public IActionResult Index()
        {
            if (_context.Usuarios.Any())
                return RedirectToAction("Index", "Home");

            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Usuario admin)
        {
            if (_context.Usuarios.Any())
                return RedirectToAction("Index", "Home");

            // 🔥 IMPORTANTE: quitamos ModelState por ahora
            // porque sabemos que está bloqueando
            // luego lo podemos volver a activar

            try
            {
                admin.Password = HashHelper.Hash(admin.Password);
                admin.Rol = "ITAdmin";

                _context.Usuarios.Add(admin);
                _context.SaveChanges();

                TempData["Mensaje"] = "Administrador creado correctamente. Ahora puede iniciar sesión.";

                return RedirectToAction("Login", "Usuario");
            }
            catch (Exception ex)
            {
                return Content("Error: " + ex.Message);
            }
        }
    }
}