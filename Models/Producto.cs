using System.Text.Json.Serialization;

namespace BoxNovaSoftAPI.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public decimal PrecioProducto { get; set; }
        public int StockProducto { get; set; }
        public string? CategoriaProducto { get; set; }
        public string? ClasificacionProducto { get; set; }
        public bool EstadoProducto { get; set; }
        
        [JsonPropertyName("categoria_nombre")]
        public string? Categoria { get; set; }

        [JsonPropertyName("categoria_detalle")]
        public CategoriaProducto categoria { get; set; }
    }
}
