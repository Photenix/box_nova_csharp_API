namespace BoxNovaSoftAPI.Models
{
    public class Cliente
    {
       
            public int IdCliente { get; set; }
            public string NombreCliente { get; set; } = null!;
            public string ApellidoCliente { get; set; } = null!;
            public string CedulaCliente { get; set; } = null!;
            public char GeneroCliente { get; set; }
            public string? DireccionCliente { get; set; }
            public string? TelCliente { get; set; }
            public string? EmailCliente { get; set; }
            public DateTime FechaRegistro { get; set; }

            public ICollection<Carrito> Carritos { get; set; } = new List<Carrito>();
        }
    }

