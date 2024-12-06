using Regulatorio.SharedKernel.Responses;

namespace Regulatorio.Domain.Response.Garantias
{
    public class ObterGarantiasResponse : BaseResponse
    {
        public ObterGarantiasResponse()
        {
            Garantias = new List<GarantiaResponse>();
        }

        public int PageIndex { get; set; }
        public int TotalItems { get; set; }
        public ICollection<GarantiaResponse> Garantias { get; set; }
    }
}
