using Regulatorio.SharedKernel.Responses;

namespace Regulatorio.Domain.Response.Dominios
{
    public sealed class ObterTipoDominioResponse : BaseResponse
    {
        public ObterTipoDominioResponse()
        {
            TipoDominios = new List<TipoDominioResponse>();
        }

        public ICollection<TipoDominioResponse> TipoDominios { get; set; }
    }
}
