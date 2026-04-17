using Microsoft.AspNetCore.Mvc;

namespace TiendaDeDisfraces.Controllers
{
    /// <summary>
    /// Controlador principal del sistema.
    /// Home es público y no requiere autenticación.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Página principal del sistema.
        /// Accesible para todos los usuarios.
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }
    }
}