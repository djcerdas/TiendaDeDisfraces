using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaDeDisfraces.Models
{
    // Modelo que representa un usuario del sistema
    public class Usuario
    {
        // Clave primaria
        public int Id { get; set; }

        // Cédula: solo números entre 9 y 12 dígitos
        [Required(ErrorMessage = "Cédula requerida")]
        [RegularExpression(@"^\d{9,12}$", ErrorMessage = "Solo números (9-12 dígitos)")]
        public string Cedula { get; set; }

        // Nombre: solo letras y espacios
        [Required(ErrorMessage = "Nombre requerido")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Solo letras")]
        public string Nombre { get; set; }

        // Correo electrónico válido
        [Required(ErrorMessage = "Correo requerido")]
        [EmailAddress(ErrorMessage = "Correo inválido")]
        public string Correo { get; set; }

        // Teléfono: exactamente 8 dígitos
        [Required(ErrorMessage = "Teléfono requerido")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Debe tener 8 dígitos")]
        public string Telefono { get; set; }

        // Usuario de acceso al sistema
        [Required(ErrorMessage = "Username requerido")]
        public string Username { get; set; }

        // Contraseña del usuario
        [Required(ErrorMessage = "Password requerido")]
        public string Password { get; set; }

        // Rol dentro del sistema (Cliente, Cajero, ITAdmin, etc.)
        public string Rol { get; set; }

        // Fecha de nacimiento del usuario
        public DateTime FechaNacimiento { get; set; }

        // Indica si el usuario es preferencial
        public bool Preferencial { get; set; }

        // Propiedad calculada: edad del usuario
        [NotMapped]
        public int Edad
        {
            get
            {
                var hoy = DateTime.Today;
                var edad = hoy.Year - FechaNacimiento.Year;
                if (FechaNacimiento.Date > hoy.AddYears(-edad)) edad--;
                return edad;
            }
        }

        // Clasificación automática según edad
        [NotMapped]
        public string TipoPublico
        {
            get
            {
                return Edad >= 18 ? "Adulto" : "Niño";
            }
        }
    }
}