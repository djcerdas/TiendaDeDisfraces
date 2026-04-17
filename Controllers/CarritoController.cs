using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace TiendaDeDisfraces.Controllers
{
    public class CarritoController : Controller
    {
        /// <summary>
        /// Valida si el usuario puede ver el carrito.
        /// Cliente NO puede acceder.
        /// </summary>
        private bool PuedeVerCarrito()
        {
            var rol = HttpContext.Session.GetString("Rol");

            return rol == "Cajero" || rol == "ITAdmin" || rol == "Supervisor";
        }

        /// <summary>
        /// Vista principal del carrito.
        /// </summary>
        public IActionResult Index()
        {
            if (!PuedeVerCarrito())
                return RedirectToAction("Index", "Home");

            return View();
        }
    }
}