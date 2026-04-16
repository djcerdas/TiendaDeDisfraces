using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TiendaDeDisfraces.Models;
using TiendaDeDisfraces.Helpers;
using Microsoft.AspNetCore.Http;

namespace TiendaDeDisfraces.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly TiendaDeDisfracesContext _context;

        public UsuarioController(TiendaDeDisfracesContext context)
        {
            _context = context;
        }

        #region LOGIN
        /// <summary>
        /// Muestra la vista de login
        /// </summary>
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Procesa el login del usuario
        /// <param name="usuarioto">Objeto usuario con datos del formulario</param>
        /// <returns>Vista correspondiente</returns>
        /// <summary>
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
                    // Guardar en sesión
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

        #endregion

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

        // CREATE POST
        [HttpPost]
        public IActionResult Create(Usuario u)
        {
            if (!ModelState.IsValid)
            {
                CargarRoles();
                return View(u);
            }

            u.Password = HashHelper.Hash(u.Password);

            _context.Usuarios.Add(u);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // EDIT GET
        public IActionResult Edit(int id)
        {
            var u = _context.Usuarios.Find(id);
            CargarRoles();
            return View(u);
        }

        // EDIT POST
        [HttpPost]
        public IActionResult Edit(Usuario u)
        {
            if (!ModelState.IsValid)
            {
                CargarRoles();
                return View(u);
            }

            var original = _context.Usuarios
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == u.Id);

            // 🔥 PASSWORD OPCIONAL
            if (string.IsNullOrWhiteSpace(u.Password))
            {
                u.Password = original.Password;
            }
            else
            {
                u.Password = HashHelper.Hash(u.Password);
            }

            _context.Update(u);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // DELETE GET
        public IActionResult Delete(int id)
        {
            return View(_context.Usuarios.Find(id));
        }

        // DELETE POST
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var u = _context.Usuarios.Find(id);
            _context.Usuarios.Remove(u);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // 🔥 ROLES
        private void CargarRoles()
        {
            ViewBag.Roles = new List<SelectListItem>
            {
                new SelectListItem { Value = "Cliente", Text = "Cliente" },
                new SelectListItem { Value = "Cajero", Text = "Cajero" },
                new SelectListItem { Value = "ITAdmin", Text = "ITAdmin" },
                new SelectListItem { Value = "Supervisor", Text = "Supervisor" }
            };
        }
    }

}