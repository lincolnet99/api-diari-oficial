
using Regulatorio.Domain.DTOs.PalavrasChave;
using Regulatorio.Domain.Request.DiarioOficial;
using Regulatorio.Domain.Request.PalavrasChave;
using Regulatorio.Domain.Response.DiarioOficial;
using Regulatorio.Domain.Response.PalavrasChave;

namespace Regulatorio.Domain.Services.PalavrasChave
{
    public interface IDiarioOficialService : IDisposable
    {
        Task<ListaPalavraChaveResponse> ObterListaPalavrasChave();
        Task<int> CriarPalavraChave(CriarPalavraChaveRequest request);
        Task<int> ExcluirPalavraChave(int palavraChaveId);
        Task<ListaArquivosProcessadosResponse> ObterListaArquivosProcessados(ConsultarListagemArquivosProcessadosRequest request);
        Task<PalavrasChavesRelevantes> PalavrasChavesRelevantes();
        Task<List<ArquivoUFData>> ObterUltimosRegistrosPorUF();
        Task<DadosDocumentos> ObterEstatisticas();
    }
}
