using Regulatorio.Domain.Request.InstituicaoFinanceira;
using Regulatorio.Domain.Response.InstituicaoFinanceira;

namespace Regulatorio.Domain.Services.InstituicaoFinanceira
{
    public interface IInstituicaoFinanceiraService : IDisposable
    {
        Task<ObterInstituicaoFinanceirasResponse> ObterInstituicoesFinanceiras(ObterInstituicoesFinanceirasRequest request);
        Task<InstituicaoFinanceiraResponse> ObterInstituicaoFinanceiraPorUf(string uf);
        Task<InstituicaoFinanceiraResponse> ObterInstituicaoFinanceiraPorId(int id);
        Task<CriarInstituicaoFinanceiraResponse> CriarInstituicaoFinanceira(CriarInstituicaoFinanceiraRequest request);
        Task<InstituicaoFinanceiraResponse> EditarInstituicaoFinanceira(EditarInstituicaoFinanceiraRequest request);
        Task<ExcluirInstituicaoFinanceiraResponse> ExcluirInstituicaoFinanceira(int idInstituicaoFinanceira);
    }
}
