namespace TiendaDeDisfraces.Models
{
    public class Producto
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public decimal Precio { get; set; }

        public string Descripcion { get; set; }

        public string Espacio { get; set; }

        // Tipo de producto: Disfraz, Accesorio, Maquillaje
        public int TipoProductoId { get; set; }

        // Público: Adulto o Niño
        public int TipoPublicoId { get; set; }

        // Solo aplica si es Disfraz
        public string Talla { get; set; }
    }
}