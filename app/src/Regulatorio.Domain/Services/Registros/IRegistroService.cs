using Regulatorio.Domain.Request.Registros;
using Regulatorio.Domain.Response.Registros;

namespace Regulatorio.Domain.Services.Registros
{
    public interface IRegistroService : IDisposable
    {
        Task<ObterRegistrosResponse> ObterRegistros(ObterRegistrosRequest request);
        Task<RegistroResponse> ObterRegistroPorUf(string uf);
        Task<RegistroResponse> ObterRegistroPorId(int id);
        Task<CriarRegistroResponse> CriarRegistro(CriarRegistroRequest request);
        Task<RegistroResponse> EditarRegistro(int idRegistro, EditarRegistroRequest request);
        Task<ExcluirRegistroResponse> ExcluirRegistro(int idRegistro);
    }
}
