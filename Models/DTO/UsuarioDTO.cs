namespace BoxNovaSoftAPI.Models.DTO
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }

        public string TarjetaIdentidad { get; set; } = null!;

        public string CorreoUsuario { get; set; } = null!;

        public string NombreUsuario { get; set; } = null!;

        public string ContrasenaUsuario { get; set; } = null!;

        public DateOnly CumpleanoUsuario { get; set; }

        public string GeneroUsuario { get; set; } = null!;

        public bool EstadoUsuario { get; set; }

        public string? Rol { get; set; }
    }
}
