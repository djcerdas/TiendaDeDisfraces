using System.ComponentModel.DataAnnotations;

namespace TiendaDeDisfraces.Models
{
    /// <summary>
    /// Modelo (Entidad) Producto.
    /// Representa la tabla Productos en la base de datos.
    /// </summary>
    public class Producto
    {
        // =========================
        // ATRIBUTOS PRIVADOS
        // =========================
        private int _id;
        private string _nombre;
        private decimal _precio;
        private string _descripcion;
        private int _tipoProductoId;
        private int _tipoPublicoId;
        private string _talla;

        // =========================
        // CONSTRUCTOR SIN PARÁMETROS
        // =========================
        public Producto()
        {
            _id = 0;
            _nombre = "";
            _precio = 0;
            _descripcion = "";
            _tipoProductoId = 0;
            _tipoPublicoId = 0;
            _talla = "";
        }

        // =========================
        // CONSTRUCTOR CON PARÁMETROS
        // =========================
        public Producto(int id, string nombre, decimal precio, string descripcion,
                        int tipoProductoId, int tipoPublicoId, string talla)
        {
            _id = id;
            _nombre = nombre;
            _precio = precio;
            _descripcion = descripcion;
            _tipoProductoId = tipoProductoId;
            _tipoPublicoId = tipoPublicoId;
            _talla = talla;
        }

        // =========================
        // PROPIEDADES (GET / SET)
        // =========================

        [Required]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "Solo letras permitidas")]
        [StringLength(120, ErrorMessage = "Máximo 120 caracteres")]
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0.01, 999999, ErrorMessage = "Debe ser un número válido")]
        public decimal Precio
        {
            get { return _precio; }
            set { _precio = value; }
        }

        [StringLength(200, ErrorMessage = "Máximo 200 caracteres")]
        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        [Required(ErrorMessage = "Debe seleccionar un tipo de producto")]
        public int TipoProductoId
        {
            get { return _tipoProductoId; }
            set { _tipoProductoId = value; }
        }

        [Required(ErrorMessage = "Debe seleccionar el público")]
        public int TipoPublicoId
        {
            get { return _tipoPublicoId; }
            set { _tipoPublicoId = value; }
        }

        [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "Talla inválida")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        public string Talla
        {
            get { return _talla; }
            set { _talla = value; }
        }
    }
}