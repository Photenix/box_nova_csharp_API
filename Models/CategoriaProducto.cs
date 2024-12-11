using BoxNovaSoftAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

[Table("CategoriaProducto")]
public class CategoriaProducto
{
    public int IdCProd { get; set; }
    public string NombreCProd { get; set; } = null!;
    public bool EstadoCProd { get; set; }

    public ICollection<SubCategoriaProducto> SubCategorias { get; set; } = new List<SubCategoriaProducto>();
}
