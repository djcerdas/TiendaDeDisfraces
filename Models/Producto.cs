using System.ComponentModel.DataAnnotations;

namespace TiendaDeDisfraces.Models
{
    // Modelo que representa un producto dentro del sistema
    public class Producto
    {
        // Identificador único del producto (PK)
        public int Id { get; set; }

        // =========================
        // NOMBRE DEL PRODUCTO
        // Solo letras y espacios
        // =========================
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Solo letras permitidas")]
        public string Nombre { get; set; }

        // =========================
        // PRECIO
        // Valores numéricos positivos
        // =========================
        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0.01, 999999, ErrorMessage = "Debe ser un número válido")]
        public decimal Precio { get; set; }

        // =========================
        // DESCRIPCIÓN
        // Campo opcional
        // =========================
        [StringLength(200, ErrorMessage = "Máximo 200 caracteres")]
        public string Descripcion { get; set; }

        // =========================
        // TIPO DE PRODUCTO
        // 1 = Disfraz
        // 2 = Accesorio
        // 3 = Maquillaje
        // =========================
        [Required(ErrorMessage = "Debe seleccionar un tipo de producto")]
        public int TipoProductoId { get; set; }

        // =========================
        // PÚBLICO OBJETIVO
        // 1 = Adulto
        // 2 = Niño
        // 3 = Todo Público
        // =========================
        [Required(ErrorMessage = "Debe seleccionar el público")]
        public int TipoPublicoId { get; set; }

        // =========================
        // TALLA
        // Solo aplica para Disfraz
        // Se controla desde la vista (dropdown + JS)
        // =========================
        [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "Talla inválida")]
        public string Talla { get; set; }
    }
}