using Regulatorio.Domain.DTOs.Normativos;
using Regulatorio.Domain.Request.Normativos;
using Regulatorio.SharedKernel.Common;

namespace Regulatorio.Domain.Repositories.Normativos
{
    public interface INormativoRepository
    {
        Task<PagedList<NormativoDto>> ObterNormativos(ObterListaNormativosRequest request);
        Task<NormativoDto> ObterNormativoPorId(int normativoId);
        Task<int> CriarNormativo(CriarNormativoRequest request);
        Task<NormativoDto> EditarNormativo(int idNormativo, EditarNormativoRequest request);
        Task<int> ExcluirNormativo(int idNormativo);
        Task<AtivarArquivarNormativoDto> AtivarArquivarNormativo(int IdNormativo);
        Task<DownloadNormativoDto> ObterNormativoParaDownload(int idNormativo);
        Task<IEnumerable<UfsRecentesDto>> ObterUfsRecentes();
        Task<int> EditarArquivoNormativo(string urlArquivo, int id);
    }
}
