using System;
using System.Collections.Generic;

namespace BoxNovaSoftAPI.Models;

public partial class Privilegio
{
    public int IdPrivilegio { get; set; }

    public string NombrePrivilegio { get; set; } = null!;

    public virtual ICollection<PerXrolXpriv> PerXrolXprivs { get; set; } = new List<PerXrolXpriv>();
}
