using System;
using System.Linq;
using TiendaDeDisfraces.Models;

namespace TiendaDeDisfraces.Helpers
{
    /// <summary>
    /// Clase helper que contiene métodos de apoyo para la lógica del sistema
    /// </summary>
    public class Service
    {
        private readonly TiendaDeDisfracesContext _context;

        #region CONSTRUCTOR

        /// <summary>
        /// Constructor que recibe el contexto de la base de datos
        /// </summary>
        /// <param name="context">Contexto de base de datos</param>
        public Service(TiendaDeDisfracesContext context)
        {
            _context = context;
        }

        #endregion

        #region LOGIN

        /// <summary>
        /// Método que valida las credenciales del usuario
        /// </summary>
        /// <param name="username">Nombre de usuario ingresado</param>
        /// <param name="password">Contraseña ingresada por el usuario</param>
        /// <returns>Usuario autenticado</returns>
        public Usuario Login(string username, string password)
        {
            var hash = HashHelper.Hash(password);

            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Username == username && u.Password == hash);

            if (usuario != null)
                return usuario;

            throw new Exception("Credenciales incorrectas");
        }

        #endregion
    }
}
