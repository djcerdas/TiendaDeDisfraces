using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using TiendaDeDisfraces.Helpers;
using TiendaDeDisfraces.Models;

namespace TiendaDeDisfraces.Controllers
{
    /// <summary>
    /// Controlador de usuarios:
    /// - Login / Logout
    /// - CRUD de usuarios
    /// </summary>
    public class UsuarioController : Controller
    {
        // Contexto de base de datos
        private readonly TiendaDeDisfracesContext _context;

        public UsuarioController(TiendaDeDisfracesContext context)
        {
            _context = context;
        }

        #region LOGIN

        /// <summary>
        /// Muestra la vista de login.
        /// Si no existen usuarios, redirige a la configuración inicial.
        /// </summary>
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
            ViewBag.Roles = new List<SelectListItem>
            {
                new SelectListItem { Value = "Cliente", Text = "Cliente" },
                new SelectListItem { Value = "Cajero", Text = "Cajero" },
                new SelectListItem { Value = "ITAdmin", Text = "IT Admin" },
                new SelectListItem { Value = "Supervisor", Text = "Supervisor" }
            };
        }

        /// <summary>
        /// Procesa el login del usuario.
        /// Si las credenciales son válidas, guarda usuario y rol en sesión.
        /// </summary>
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

                ModelState.AddModelError("", "Usuario o contraseña incorrectos");
                return View();
            }
            catch
            {
                ModelState.AddModelError("", "Ocurrió un error al iniciar sesión");
                return View();
            }
        }

        /// <summary>
        /// Cierra la sesión actual.
        /// </summary>
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region CRUD

        /// <summary>
        /// Lista todos los usuarios registrados.
        /// </summary>
        public IActionResult Index()
        {
            return View(_context.Usuarios.ToList());
        }

        /// <summary>
        /// Muestra la vista de creación de usuario.
        /// </summary>
        public IActionResult Create()
        {
            CargarRoles();
            return View();
        }

        /// <summary>
        /// Guarda un nuevo usuario.
        /// Aplica validación del modelo y encripta la contraseña.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Usuario u)
        {
            if (!ModelState.IsValid)
            {
                CargarRoles();
                return View(u);
            }

            // Solo los clientes pueden quedar como preferenciales
            if (u.Rol != "Cliente")
            {
                u.Preferencial = false;
            }

            // Encripta la contraseña antes de guardar
            u.Password = HashHelper.Hash(u.Password);

            _context.Usuarios.Add(u);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Muestra la vista de edición de un usuario existente.
        /// </summary>
        public IActionResult Edit(int id)
        {
            var usuario = _context.Usuarios.Find(id);

            if (usuario == null)
            {
                return NotFound();
            }

            CargarRoles();

            // Se deja vacío para que el usuario ingrese una nueva contraseña
            // solo si desea cambiarla.
            usuario.Password = "";

            return View(usuario);
        }

        /// <summary>
        /// Actualiza un usuario existente.
        /// Si no se escribe una nueva contraseña, conserva la actual.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Usuario u)
        {
            if (!ModelState.IsValid)
            {
                CargarRoles();
                return View(u);
            }

            var usuarioDB = _context.Usuarios.Find(u.Id);

            if (usuarioDB == null)
            {
                return NotFound();
            }

            // Actualización de campos editables
            usuarioDB.Cedula = u.Cedula;
            usuarioDB.Nombre = u.Nombre;
            usuarioDB.Correo = u.Correo;
            usuarioDB.Telefono = u.Telefono;
            usuarioDB.Username = u.Username;
            usuarioDB.Rol = u.Rol;
            usuarioDB.FechaNacimiento = u.FechaNacimiento;

            // Solo los clientes pueden ser preferenciales
            usuarioDB.Preferencial = (u.Rol == "Cliente") ? u.Preferencial : false;

            // Solo reemplaza la contraseña si el usuario escribió una nueva
            if (!string.IsNullOrWhiteSpace(u.Password))
            {
                usuarioDB.Password = HashHelper.Hash(u.Password);
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Muestra la vista de confirmación para eliminar un usuario.
        /// </summary>
        public IActionResult Delete(int id)
        {
            var usuario = _context.Usuarios.Find(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        /// <summary>
        /// Elimina definitivamente un usuario.
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var usuario = _context.Usuarios.Find(id);

            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        #endregion
    }
}