using Regulatorio.SharedKernel.Responses;

namespace Regulatorio.Domain.Response.Normativos
{
    public class ObterNormativosResponse : BaseResponse
    {
        public ObterNormativosResponse()
        {
            Normativos = new List<NormativoResponse>();
        }

        public int PageIndex { get; set; }
        public int TotalItems { get; set; }
        public ICollection<NormativoResponse> Normativos { get; set; }
    }
}
