using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaDeDisfraces.Models
{
    public class Usuario
    {
        public int Id { get; set; } // PK

        [Required(ErrorMessage = "Cédula requerida")]
        [RegularExpression(@"^\d{9,12}$", ErrorMessage = "Solo números (9-12 dígitos)")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "Nombre requerido")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Solo letras")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Correo requerido")]
        [EmailAddress(ErrorMessage = "Correo inválido")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "Teléfono requerido")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Debe tener 8 dígitos")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Username requerido")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password requerido")]
        public string Password { get; set; }

        public string Rol { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public bool Preferencial { get; set; } // ⭐ Solo clientes

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