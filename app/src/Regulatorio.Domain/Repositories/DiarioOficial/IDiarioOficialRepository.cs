
using Regulatorio.Domain.DTOs.DiarioOficial;
using Regulatorio.Domain.DTOs.PalavrasChave;
using Regulatorio.Domain.Request.DiarioOficial;
using Regulatorio.Domain.Request.PalavrasChave;
using Regulatorio.Domain.Response.DiarioOficial;
using Regulatorio.SharedKernel.Common;

namespace Regulatorio.Domain.Repositories.PalavrasChave
{
    public interface IDiarioOficialRepository
    {
        Task<List<PalavraChaveDto>> ObterPalavrasChave();
        Task<int> IncluirPalavraChave(CriarPalavraChaveRequest request);
        Task<int> ApagarPalavraChave(int palavraChaveId);
        Task<PagedList<ArquivoProcessadoDto>> ObterArquivosProcessados(ConsultarListagemArquivosProcessadosRequest request);
        Task<PalavrasChavesRelevantes> ObterPalavrasChaveRelevantes();
        Task<List<ArquivoUFData>> ObterUltimosRegistrosPorUF();
        Task<DadosDocumentos> ObterEstatisticas();

    }
}
