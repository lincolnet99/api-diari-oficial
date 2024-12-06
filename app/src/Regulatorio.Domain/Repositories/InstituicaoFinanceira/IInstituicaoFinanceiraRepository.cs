using Regulatorio.Domain.DTOs.InstituicaoFinanceira;
using Regulatorio.Domain.Request.InstituicaoFinanceira;
using Regulatorio.SharedKernel.Common;

namespace Regulatorio.Domain.Repositories.InstituicaoFinanceiraRepository
{
    public interface IInstituicaoFinanceiraRepository
    {
        Task<PagedList<InstituicaoFinanceiraDto>> ObterInstituicoesFinanceiras(ObterInstituicoesFinanceirasRequest request);
        Task<int> CriarInstituicaoFinanceira(CriarInstituicaoFinanceiraRequest request);
        Task<InstituicaoFinanceiraDto> ObterInstituicaoFinanceiraPorUf(string uf);
        Task<InstituicaoFinanceiraDto> ObterInstituicaoFinanceiraPorId(int id);
        Task<InstituicaoFinanceiraDto> EditarInstituicaoFinanceira(EditarInstituicaoFinanceiraRequest request);
        Task<InstituicaoFinanceiraDto> EditarInstituicaoFinanceiraDocumentos(EditarInstituicaoFinanceiraRequest request);
        Task<int> ExcluirInstituicaoFinanceira(int idInstituicaoFinanceira);
        Task<bool> CriarInstituicaoFinanceiraDocumentos(List<string> documentos, int instituicaoFinanceira);

    }
}
