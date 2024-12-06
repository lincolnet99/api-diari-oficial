using Regulatorio.Domain.DTOs.Garantias;
using Regulatorio.Domain.DTOs.Registros;
using Regulatorio.Domain.Request.Garantias;
using Regulatorio.SharedKernel.Common;

namespace Regulatorio.Domain.Repositories.Garantias
{
    public interface IGarantiaRepository
    {
        Task<PagedList<GarantiaDto>> ObterGarantias(ObterGarantiasRequest request);
        Task<int> CriarGarantia(CriarGarantiaRequest request);
        Task<GarantiaDto> ObterGarantiaPorUf(string uf);
        Task<GarantiaDto> ObterGarantiaPorId(int id);
        Task<GarantiaDto> EditarGarantia(int idGarantia, EditarGarantiaRequest request);
        Task<int> ExcluirGarantia(int idGarantia);
    }
}
