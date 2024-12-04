using System;
using System.Collections.Generic;

namespace BoxNovaSoftAPI.Models;

public partial class PerXrolXpriv
{
    public int IdPerxrol { get; set; }

    public int? IdPer { get; set; }

    public int? IdRol { get; set; }

    public int? IdPriv { get; set; }

    public virtual Permiso? IdPerNavigation { get; set; }

    public virtual Privilegio? IdPrivNavigation { get; set; }

    public virtual Rol? IdRolNavigation { get; set; }
}
