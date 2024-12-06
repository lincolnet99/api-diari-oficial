using Regulatorio.Domain.DTOs.Registros;
using Regulatorio.Domain.Request.Registros;
using Regulatorio.SharedKernel.Common;

namespace Regulatorio.Domain.Repositories.Registros
{
    public interface IRegistroRepository
    {
        Task<PagedList<RegistroDto>> ObterRegistros(ObterRegistrosRequest request);
        Task<int> CriarRegistro(CriarRegistroRequest request);
        Task<RegistroDto> ObterRegistroPorUf(string uf);
        Task<RegistroDto> ObterRegistroPorId(int id);
        Task<RegistroDto> EditarRegistro(int idRegistro, EditarRegistroRequest request);
        Task<int> ExcluirRegistro(int idRegistro);
    }
}
