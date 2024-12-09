namespace BoxNovaSoftAPI.Models
{
    public class SubCategoriaProducto
    {
        public int IdSubCProd { get; set; }
        public string NombreCProd { get; set; } = null!;
        public bool EstadoCProd { get; set; }
        public int IdCProd { get; set; }

        public CategoriaProducto Categoria { get; set; } = null!;
    }
}
