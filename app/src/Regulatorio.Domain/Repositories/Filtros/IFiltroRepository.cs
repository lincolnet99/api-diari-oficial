using Regulatorio.Domain.Entities.Filtros;

namespace Regulatorio.Domain.Repositories.Filtros
{
    public interface IFiltroRepository
    {
        Task<IEnumerable<TipoNormativo>> ObterTipoNormativo();
    }
}
