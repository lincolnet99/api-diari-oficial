using Regulatorio.Domain.Repositories.Filtros;
using Regulatorio.Domain.Response.Filtros;
using Regulatorio.Domain.Services.Filtros;
using Regulatorio.SharedKernel.Services;

namespace Regulatorio.ApplicationService.Services.Filtros
{
    public class FiltroAppService : BaseService, IFiltroService
    {

        private readonly IFiltroRepository _filtroRepository;

        public FiltroAppService(IFiltroRepository filtroRepository)
        {
            _filtroRepository = filtroRepository;
        }

        public async Task<ObterTiposNormativoResponse> ObterTipoNormativo()
        {
            var response = new ObterTiposNormativoResponse();

            var tipos = await _filtroRepository.ObterTipoNormativo();

            if (tipos == null)
            {
                response.AddError("404", "Nenhuma garantia encontrada");
                return response;
            }

            foreach (var tipo in tipos)
            {
                response.TiposNormativo.Add(tipo);
            }

            return response;
        }
    }
}
