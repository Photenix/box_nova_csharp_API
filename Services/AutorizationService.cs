using BoxNovaSoftAPI.Models;
using BoxNovaSoftAPI.Models.Customs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BoxNovaSoftAPI.Services
{
    public class AutorizationService : IAutorizationService
    {
        private readonly BoxNovaDbContext _context;
        private readonly IConfiguration _configuration;

        public AutorizationService(BoxNovaDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /*
        public Task<AutorizationResponse> DevolverToken(AutorizacionRequest autorizacion)
        {
            throw new NotImplementedException();
        }
        */
        
        public async Task<AutorizationResponse> DevolverToken( AutorizacionRequest autorizacion)
        {
            var usuario_encontrado = _context.Usuarios.FirstOrDefault(x =>
                x.CorreoUsuario == autorizacion.CorreoUsuario && x.ContrasenaUsuario == autorizacion.ContrasenaUsuario
            );

            if( usuario_encontrado == null)
            {
                return await Task.FromResult<AutorizationResponse>(null); 
            }

            string tokenCreado = GenerarToken(usuario_encontrado.IdUsuario.ToString());

            return new AutorizationResponse()
            {
                Token = tokenCreado,
                Resultado = true,
                Mensaje = "Ok"
            };
        }


        private string GenerarToken( string idUsuario)
        {
            var key = _configuration.GetValue<string>("JwtSettings:key");

            //var keyBytes = Encoding.ASCII.GetBytes(key);
            var keyBytes = Encoding.UTF8.GetBytes(key);

            var claims = new ClaimsIdentity();

            claims.AddClaim( new Claim(ClaimTypes.NameIdentifier, idUsuario) );

            var credencialesToken = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256
            );

            var tokenDescriptior = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = credencialesToken
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken( tokenDescriptior );
            string tokenCreado = tokenHandler.WriteToken(tokenConfig );
            return tokenCreado;
        }
    }
}
