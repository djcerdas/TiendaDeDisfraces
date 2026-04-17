using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TiendaDeDisfraces.Helpers;
using TiendaDeDisfraces.Models;

namespace TiendaDeDisfraces.Controllers
{
    /// <summary>
    /// Controlador de usuarios: Login + CRUD
    /// </summary>
    public class UsuarioController : Controller
    {
        private readonly TiendaDeDisfracesContext _context;

        public UsuarioController(TiendaDeDisfracesContext context)
        {
            _context = context;
        }

        #region LOGIN

        public IActionResult Login()
        {
            if (!_context.Usuarios.Any())
                return RedirectToAction("Index", "Setup");

            return View();
        }

        /// <summary>
        /// Carga los roles para los DropDownList en las vistas Create y Edit.
        /// </summary>
        private void CargarRoles()
        {
            ViewBag.Roles = new List<SelectListItem>   // ✔ NOMBRE CORRECTO
    {
        new SelectListItem { Value = "Cliente", Text = "Cliente" },
        new SelectListItem { Value = "Cajero", Text = "Cajero" },
        new SelectListItem { Value = "ITAdmin", Text = "IT Admin" },
        new SelectListItem { Value = "Supervisor", Text = "Supervisor" }
    };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Usuario usuarioto)
        {
            try
            {
                Service service = new Service(_context);

                var usuarioLogin = service.Login(usuarioto.Username, usuarioto.Password);

                if (usuarioLogin != null)
                {
                    HttpContext.Session.SetString("Usuario", usuarioLogin.Username);
                    HttpContext.Session.SetString("Rol", usuarioLogin.Rol);

                    return RedirectToAction("Index", "Home");
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region CRUD

        // LIST
        public IActionResult Index()
        {
            return View(_context.Usuarios.ToList());
        }

        // CREATE GET
        public IActionResult Create()
        {
            CargarRoles(); 
            return View();
        }

        // EDIT GET
        public IActionResult Edit(int id)
        {
            var usuario = _context.Usuarios.Find(id);

            CargarRoles(); // 🔥 IMPORTANTE

            usuario.Password = "";

            return View(usuario);
        }

        // EDIT POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Usuario u)
        {
            if (!ModelState.IsValid)
            {
                CargarRoles(); 
                return View(u);
            }

            // Se encripta la contraseña antes de guardar
            u.Password = HashHelper.Hash(u.Password);

            _context.Usuarios.Add(u);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // DELETE GET
        public IActionResult Delete(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            return View(usuario);
        }

        // DELETE POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var usuario = _context.Usuarios.Find(id);

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        #endregion
    }
}