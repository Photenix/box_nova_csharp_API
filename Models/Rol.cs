using System;
using System.Collections.Generic;

namespace BoxNovaSoftAPI.Models;

public partial class Rol
{
    public int IdRol { get; set; }

    public string NombreRol { get; set; } = null!;

    public bool EstadoRol { get; set; }

    public virtual ICollection<PerXrolXpriv> PerXrolXprivs { get; set; } = new List<PerXrolXpriv>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
