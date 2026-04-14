using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TiendaDeDisfraces.Models;
using TiendaDeDisfraces.Helpers;

namespace TiendaDeDisfraces.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly TiendaDeDisfracesContext _c;

        public UsuarioController(TiendaDeDisfracesContext c)
        {
            _c = c;
        }

        // LIST
        public IActionResult Index()
        {
            return View(_c.Usuarios.ToList());
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

            _c.Usuarios.Add(u);
            _c.SaveChanges();

            return RedirectToAction("Index");
        }

        // EDIT GET
        public IActionResult Edit(int id)
        {
            var u = _c.Usuarios.Find(id);
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

            var original = _c.Usuarios
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

            _c.Update(u);
            _c.SaveChanges();

            return RedirectToAction("Index");
        }

        // DELETE GET
        public IActionResult Delete(int id)
        {
            return View(_c.Usuarios.Find(id));
        }

        // DELETE POST
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var u = _c.Usuarios.Find(id);
            _c.Usuarios.Remove(u);
            _c.SaveChanges();

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