using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using TiendaDeDisfraces.Models;

namespace TiendaDeDisfraces.Controllers
{
    public class ProductoController : Controller
    {
        // Contexto de base de datos
        private readonly TiendaDeDisfracesContext _c;

        public ProductoController(TiendaDeDisfracesContext c)
        {
            _c = c;
        }

        // =========================
        // LISTADO DE PRODUCTOS
        // =========================
        public IActionResult Index()
        {
            return View(_c.Productos.ToList());
        }

        // =========================
        // CREATE (GET)
        // Carga los combos del formulario
        // =========================
        public IActionResult Create()
        {
            CargarCombos();
            return View();
        }

        // =========================
        // CREATE (POST)
        // 🔴 FIX: validación antes de guardar
        // =========================
        [HttpPost]
        public IActionResult Create(Producto p)
        {
            // Validar selección de dropdowns
            if (p.TipoProductoId == 0)
            {
                ModelState.AddModelError("TipoProductoId", "Seleccione un tipo de producto");
            }

            if (p.TipoPublicoId == 0)
            {
                ModelState.AddModelError("TipoPublicoId", "Seleccione un público");
            }

            // Si hay errores → regresar a vista
            if (!ModelState.IsValid)
            {
                CargarCombos(); // 🔴 obligatorio en MVC
                return View(p);
            }

            // Guardado en base de datos
            _c.Productos.Add(p);
            _c.SaveChanges();

            return RedirectToAction("Index");
        }

        // =========================
        // EDIT (GET)
        // =========================
        public IActionResult Edit(int id)
        {
            CargarCombos();
            return View(_c.Productos.Find(id));
        }

        // =========================
        // EDIT (POST)
        // =========================
        [HttpPost]
        public IActionResult Edit(Producto p)
        {
            if (p.TipoProductoId == 0)
            {
                ModelState.AddModelError("TipoProductoId", "Seleccione un tipo de producto");
            }

            if (p.TipoPublicoId == 0)
            {
                ModelState.AddModelError("TipoPublicoId", "Seleccione un público");
            }

            if (!ModelState.IsValid)
            {
                CargarCombos();
                return View(p);
            }

            _c.Update(p);
            _c.SaveChanges();

            return RedirectToAction("Index");
        }

        // =========================
        // DELETE
        // =========================
        public IActionResult Delete(int id)
        {
            return View(_c.Productos.Find(id));
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var p = _c.Productos.Find(id);
            _c.Productos.Remove(p);
            _c.SaveChanges();

            return RedirectToAction("Index");
        }

        // =========================
        // MÉTODO AUXILIAR
        // Evita duplicación de código
        // =========================
        private void CargarCombos()
        {
            ViewBag.TiposProducto = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Disfraz" },
                new SelectListItem { Value = "2", Text = "Accesorio" },
                new SelectListItem { Value = "3", Text = "Maquillaje" }
            };

            ViewBag.TiposPublico = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Adulto" },
                new SelectListItem { Value = "2", Text = "Niño" },
                new SelectListItem { Value = "3", Text = "Todo Público" }
            };
        }
    }
}