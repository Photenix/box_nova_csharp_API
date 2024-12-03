using System;
using System.Collections.Generic;

namespace BoxNovaSoftAPI.Models;

public partial class Permiso
{
    public int IdPermiso { get; set; }

    public string NombrePermiso { get; set; } = null!;

    public bool EstadoPermiso { get; set; }

    public virtual ICollection<PerXrolXpriv> PerXrolXprivs { get; set; } = new List<PerXrolXpriv>();
}
