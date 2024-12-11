namespace BoxNovaSoftAPI.Models
{
    public class SubCategoriaProducto
    {
        public int IdSubCProd { get; set; }
        public string NombreSubCProd { get; set; } = null!;
        public bool EstadoSubCProd { get; set; }

        public int IdCProd { get; set; } // Clave foránea
        public CategoriaProducto CategoriaProducto { get; set; }  // Propiedad de navegación
    }


}
