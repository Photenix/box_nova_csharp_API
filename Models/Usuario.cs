using System;
using System.Collections.Generic;

namespace BoxNovaSoftAPI.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string TarjetaIdentidad { get; set; } = null!;

    public string CorreoUsuario { get; set; } = null!;

    public string NombreUsuario { get; set; } = null!;

    public string ContrasenaUsuario { get; set; } = null!;

    public DateOnly CumpleanoUsuario { get; set; }

    public DateOnly FechaCreacionUsuario { get; set; }

    public string GeneroUsuario { get; set; } = null!;

    public string EstadoUsuario { get; set; } = null!;

    public int FkRol { get; set; }

    public virtual Role FkRolNavigation { get; set; } = null!;
}
