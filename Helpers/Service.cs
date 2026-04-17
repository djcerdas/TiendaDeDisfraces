using System;
using System.Collections.Generic;
using System.Linq;
using TiendaDeDisfraces.Models;

namespace TiendaDeDisfraces.Helpers
{
    /// <summary>
    /// Clase helper que contiene la lógica del sistema (estilo profesora)
    /// </summary>
    public class Service
    {
        private readonly TiendaDeDisfracesContext _context;

        #region CONSTRUCTOR

        /// <summary>
        /// Constructor que recibe el contexto de la base de datos
        /// </summary>
        public Service(TiendaDeDisfracesContext context)
        {
            _context = context;
        }

        #endregion

        #region LOGIN

        /// <summary>
        /// Valida credenciales del usuario
        /// </summary>
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

        #region CRUD USUARIO

        /// <summary>
        /// Obtiene la lista de usuarios
        /// </summary>
        public List<Usuario> mostrarUsuario()
        {
            return _context.Usuarios.ToList();
        }

        /// <summary>
        /// Agrega un nuevo usuario
        /// </summary>
        public void agregarUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        /// <summary>
        /// Busca un usuario por ID
        /// </summary>
        public Usuario buscarUsuario(int id)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Id == id);
        }

        /// <summary>
        /// Actualiza un usuario existente
        /// </summary>
        public void actualizarUsuario(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
        }

        /// <summary>
        /// Elimina un usuario por ID
        /// </summary>
        public void eliminarUsuario(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);

            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                _context.SaveChanges();
            }
        }

        #endregion
    }
}