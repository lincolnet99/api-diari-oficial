using Regulatorio.Domain.Enum.Dominios;
using Regulatorio.Domain.Request.Dominios;
using Regulatorio.Domain.Response.Dominios;

namespace Regulatorio.Domain.Services.Dominios
{
    public interface IDominioService : IDisposable
    {
        Task<DominioResponse> ObterDominio(TipoDominioEnum tipoDominio);
        Task<CriarTipoDominioResponse> CriarTipoDominio(CriarTipoDominioRequest request);
        Task<CriarDominioResponse> CriarDominio(CriarDominioRequest request);
        Task<ObterTipoDominioResponse> ObterTipoDominio(Guid usuarioGuid);
        Task<DominioResponse> ObterDominioPorTipo(Guid usuarioGuid, string tipoDominio);
        Task<DominioResponse> ObterDominioPorId(int dominioId);
    }
}
