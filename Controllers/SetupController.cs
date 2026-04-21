using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using TiendaDeDisfraces.Helpers;
using TiendaDeDisfraces.Models;

namespace TiendaDeDisfraces.Controllers
{
    // Controlador encargado de la configuración inicial del sistema.
    // Su propósito es permitir la creación del primer usuario administrador.
    public class SetupController : Controller
    {
        // Contexto de base de datos del sistema
        private readonly TiendaDeDisfracesContext _context;

        public SetupController(TiendaDeDisfracesContext context)
        {
            _context = context;
        }

        // GET: Setup/Index
        // Muestra la pantalla de configuración inicial solo si no existen usuarios en la base de datos.
        public IActionResult Index()
        {
            // Si ya existe al menos un usuario, no se debe volver a mostrar Setup.
            if (_context.Usuarios.Any())
                return RedirectToAction("Index", "Home");

            // Si no hay usuarios, se muestra la vista para crear el primer administrador.
            return View();
        }

        // POST: Setup/Index
        // Procesa el formulario para crear el primer administrador del sistema.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Usuario admin)
        {
            // Si ya existe un usuario, bloquea la creación adicional desde Setup.
            if (_context.Usuarios.Any())
                return RedirectToAction("Index", "Home");

            try
            {
                // Encripta la contraseña antes de guardarla en la base de datos.
                admin.Password = HashHelper.Hash(admin.Password);

                // Fuerza el rol del primer usuario a ITAdmin.
                admin.Rol = "ITAdmin";

                // Guarda el nuevo administrador en la base de datos.
                _context.Usuarios.Add(admin);
                _context.SaveChanges();

                // Mensaje temporal para informar al usuario que el administrador fue creado correctamente.
                TempData["Mensaje"] = "Administrador creado correctamente. Ahora puede iniciar sesión.";

                // Redirige al login para que el administrador ingrese al sistema.
                return RedirectToAction("Login", "Usuario");
            }
            catch (Exception ex)
            {
                // Si ocurre un error, muestra el mensaje para facilitar la depuración.
                return Content("Error: " + ex.Message);
            }
        }
    }
}