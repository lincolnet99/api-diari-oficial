using Regulatorio.Domain.Response.Filtros;

namespace Regulatorio.Domain.Services.Filtros
{
    public interface IFiltroService : IDisposable
    {
         Task<ObterTiposNormativoResponse> ObterTipoNormativo();
    }
}
