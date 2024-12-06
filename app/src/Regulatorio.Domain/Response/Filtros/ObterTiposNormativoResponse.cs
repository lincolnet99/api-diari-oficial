using Regulatorio.Domain.Entities.Filtros;
using Regulatorio.SharedKernel.Responses;

namespace Regulatorio.Domain.Response.Filtros
{
    public class ObterTiposNormativoResponse : BaseResponse
    {
        public ObterTiposNormativoResponse()
        {
            TiposNormativo = new List<TipoNormativo>();
        }

        public ICollection<TipoNormativo> TiposNormativo { get; set; }
    }
}
