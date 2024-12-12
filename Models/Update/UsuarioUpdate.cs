using System;
using System.Collections.Generic;

namespace BoxNovaSoftAPI.Models;

public partial class UsuarioUpdate
{
    public string? TarjetaIdentidad { get; set; } = null!;

    public string? CorreoUsuario { get; set; } = null!;

    public string? NombreUsuario { get; set; } = null!;

    public string? ContrasenaUsuario { get; set; } = null!;

    public DateOnly? CumpleanoUsuario { get; set; }

    public string? GeneroUsuario { get; set; } = null!;

    public bool? EstadoUsuario { get; set; }

    public int? FkRol { get; set; }

    public virtual Rol? FkRolNavigation { get; set; }
}
