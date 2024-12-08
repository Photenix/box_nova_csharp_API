using BoxNovaSoftAPI.Models.Customs;

namespace BoxNovaSoftAPI.Services
{
    public interface IAutorizationService
    {
        Task<AutorizationResponse> DevolverToken (AutorizacionRequest autorizacion);
    }
}
