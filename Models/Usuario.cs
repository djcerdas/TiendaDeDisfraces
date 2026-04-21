using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaDeDisfraces.Models
{
    /// <summary>
    /// Modelo (Entidad) Usuario.
    /// Esta clase representa la tabla Usuarios en la base de datos.
    /// </summary>
    public class Usuario
    {
        // Atributos privados
        private int _id;
        private string _cedula;
        private string _nombre;
        private string _correo;
        private string _telefono;
        private string _username;
        private string _password;
        private string _rol;
        private DateTime _fechaNacimiento;
        private bool _preferencial;

        // Constructor sin parámetros (necesario para MVC/Entity)
        public Usuario()
        {
            _id = 0;
            _cedula = "";
            _nombre = "";
            _correo = "";
            _telefono = "";
            _username = "";
            _password = "";
            _rol = "";
            _fechaNacimiento = DateTime.Today;
            _preferencial = false;
        }

        // Constructor con parámetros
        public Usuario(int id, string cedula, string nombre, string correo, string telefono,
                       string username, string password, string rol, DateTime fechaNacimiento,
                       bool preferencial)
        {
            _id = id;
            _cedula = cedula;
            _nombre = nombre;
            _correo = correo;
            _telefono = telefono;
            _username = username;
            _password = password;
            _rol = rol;
            _fechaNacimiento = fechaNacimiento;
            _preferencial = preferencial;
        }

        // Propiedades públicas (Getters/Setters)

        [Required]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [Required(ErrorMessage = "Cédula requerida")]
        [RegularExpression(@"^\d{9,12}$", ErrorMessage = "Solo números (9-12 dígitos)")]
        [StringLength(12, ErrorMessage = "La cédula no puede superar 12 caracteres.")]
        public string Cedula
        {
            get { return _cedula; }
            set { _cedula = value; }
        }

        [Required(ErrorMessage = "Nombre requerido")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "Solo letras y espacios")]
        [StringLength(120, ErrorMessage = "El nombre no puede superar 120 caracteres.")]
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        [Required(ErrorMessage = "Correo requerido")]
        [EmailAddress(ErrorMessage = "Correo inválido")]
        [StringLength(150, ErrorMessage = "El correo no puede superar 150 caracteres.")]
        public string Correo
        {
            get { return _correo; }
            set { _correo = value; }
        }

        [Required(ErrorMessage = "Teléfono requerido")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Debe tener 8 dígitos")]
        [StringLength(8, ErrorMessage = "El teléfono no puede superar 8 caracteres.")]
        public string Telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }

        [Required(ErrorMessage = "Username requerido")]
        [StringLength(50, ErrorMessage = "El username no puede superar 50 caracteres.")]
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        [Required(ErrorMessage = "Password requerido")]
        [StringLength(255, ErrorMessage = "La contraseña no puede superar 255 caracteres.")]
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        [StringLength(50, ErrorMessage = "El rol no puede superar 50 caracteres.")]
        public string Rol
        {
            get { return _rol; }
            set { _rol = value; }
        }

        [DataType(DataType.Date)]
        public DateTime FechaNacimiento
        {
            get { return _fechaNacimiento; }
            set { _fechaNacimiento = value; }
        }

        public bool Preferencial
        {
            get { return _preferencial; }
            set { _preferencial = value; }
        }

        // Propiedad calculada: edad del usuario
        [NotMapped]
        public int Edad
        {
            get
            {
                var hoy = DateTime.Today;
                var edad = hoy.Year - FechaNacimiento.Year;

                if (FechaNacimiento.Date > hoy.AddYears(-edad))
                {
                    edad--;
                }

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