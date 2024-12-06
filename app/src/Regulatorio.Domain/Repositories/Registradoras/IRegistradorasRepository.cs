using Regulatorio.Domain.DTOs.Registradora;
using Regulatorio.Domain.Request.Registradoras;
using Regulatorio.SharedKernel.Common;

namespace Regulatorio.Domain.Repositories.RegistradoraRepository
{
    public interface IRegistradoraRepository
    {
        Task<PagedList<RegistradoraDto>> ObterRegistradoras(ObterRegistradorasRequest request);
        Task<PagedList<RegistradoraDto>> CriarRegistradora(CriarRegistradoraRequest request);
        Task<PagedList<RegistradoraDto>> ObterRegistradoraPorUf(string uf);
        Task<RegistradoraDto> ObterRegistradoraPorId(int id);
        Task<RegistradoraDto> EditarRegistradora(EditarRegistradoraRequest request, string fileName, string url);
        Task<int> ExcluirRegistradora(int idRegistradora);
        Task<PagedList<RegistradoraDto>> ObterRegistradoraPorEmpresa(string empresa);
        Task<int> EditarArquivoRegistradora(string urlArquivo, int id, string fileName);
        Task<DownloadRegistratoraDto> ObterRegistradoraParaDownload(int id);
    }
}
