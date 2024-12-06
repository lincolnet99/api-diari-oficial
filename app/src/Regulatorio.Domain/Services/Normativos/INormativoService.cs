using Regulatorio.Domain.Request.Normativos;
using Regulatorio.Domain.Response.Normativos;

namespace Regulatorio.Domain.Services.Normativos
{
    public interface INormativoService : IDisposable
    {
        Task<ObterNormativosResponse> ObterNormativos(ObterListaNormativosRequest request);
        Task<NormativoResponse> ObterNormativoPorId(int idNormativo);
        Task<NormativoResponse> EditarNormativo(int idNormativo, EditarNormativoRequest request);
        Task<CriarNormativoResponse> CriarNormativo(CriarNormativoRequest request);
        Task<AtivarArquivarNormativoResponse> AtivarArquivarNormativo(int idNormativo);
        Task<ExcluirNormativoResponse> ExcluirNormativo(int idNormativo);
        Task<ObterNormativoParaDownloadResponse> ObterNormativoParaDownload(int idNormativo);
        Task<ObterUfsRecentesResponse> ObterUfsRecentes();
    }
}
