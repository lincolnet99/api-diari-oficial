using Regulatorio.SharedKernel.Requests;

namespace Regulatorio.Domain.Request.Garantias
{
    public class ObterGarantiasRequest : BaseEntityRequest
    {
        public ObterGarantiasRequest()
        {
            PageIndex = 0;
            PageSize = 10;
        }
        public string? TipoRegistro { get; set; }
        public string[] Uf { get; set; } = Array.Empty<string>();
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public string? Sort { get; set; }
    }
}
