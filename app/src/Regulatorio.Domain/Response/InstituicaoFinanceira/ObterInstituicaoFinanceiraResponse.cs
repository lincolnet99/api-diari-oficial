using Regulatorio.SharedKernel.Responses;

namespace Regulatorio.Domain.Response.InstituicaoFinanceira
{
    public class ObterInstituicaoFinanceirasResponse : BaseResponse
    {
        public ObterInstituicaoFinanceirasResponse()
        {
            InstituicoesFinanceiras = new List<InstituicaoFinanceiraResponse>();
        }

        public int PageIndex { get; set; }
        public int TotalItems { get; set; }
        public ICollection<InstituicaoFinanceiraResponse> InstituicoesFinanceiras { get; set; }
    }
}
