using Regulatorio.Domain.Request.Garantias;
using Regulatorio.Domain.Response.Garantias;

namespace Regulatorio.Domain.Services.Garantias
{
    public interface IGarantiaService : IDisposable
    {
        Task<ObterGarantiasResponse> ObterGarantias(ObterGarantiasRequest request);
        Task<GarantiaResponse> ObterGarantiaPorUf(string uf);
        Task<GarantiaResponse> ObterGarantiaPorId(int id);
        Task<CriarGarantiaResponse> CriarGarantia(CriarGarantiaRequest request);
        Task<GarantiaResponse> EditarGarantia(int idGarantia, EditarGarantiaRequest request);
        Task<ExcluirGarantiaResponse> ExcluirGarantia(int idGarantia);
    }
}
