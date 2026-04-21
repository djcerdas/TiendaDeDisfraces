using System;

namespace TiendaDeDisfraces.Models
{
    /// <summary>
    /// Modelo (Entidad) Cliente.
    /// Hereda de Usuario y representa un cliente dentro del sistema.
    /// </summary>
    public class Cliente : Usuario
    {
        // =========================
        // CONSTRUCTOR SIN PARÁMETROS
        // =========================
        public Cliente() : base()
        {
        }

        // =========================
        // CONSTRUCTOR CON PARÁMETROS
        // =========================
        public Cliente(int id, string cedula, string nombre, string correo, string telefono,
                       string username, string password, string rol, DateTime fechaNacimiento,
                       bool preferencial)
            : base(id, cedula, nombre, correo, telefono, username, password, rol, fechaNacimiento, preferencial)
        {
        }
    }
}