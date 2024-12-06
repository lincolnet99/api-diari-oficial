using Regulatorio.Domain.Request.Registradoras;
using Regulatorio.Domain.Response.Registradoras;

namespace Regulatorio.Domain.Services.Registradoras
{
    public interface IRegistradorasService : IDisposable
    {
        Task<ObterRegistradorasResponse> ObterRegistradoras(ObterRegistradorasRequest request);
        Task<ObterRegistradorasPorUfResponse> ObterRegistradoraPorUf(string uf);
        Task<RegistradorasResponse> ObterRegistradoraPorId(int id);
        Task<CriarRegistradorasResponse> CriarRegistradora(CriarRegistradoraRequest request);
        Task<RegistradorasResponse> EditarRegistradora(int id, EditarRegistradoraRequest request);
        Task<ExcluirRegistradorasResponse> ExcluirRegistradora(int idRegistradora);
        Task<ObterRegistradorasPorEmpresaResponse> ObterRegistradoraPorEmpresa(string empresa);
        Task<ObterRegistradoraParaDownloadResponse> ObterRegistradoraParaDownload(int idRegistradora);
    }
}
