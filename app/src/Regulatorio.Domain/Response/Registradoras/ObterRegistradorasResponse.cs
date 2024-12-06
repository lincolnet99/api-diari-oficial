using Regulatorio.SharedKernel.Responses;

namespace Regulatorio.Domain.Response.Registradoras
{
    public class ObterRegistradorasResponse : BaseResponse
    {
        public ObterRegistradorasResponse()
        {
            Registradoras = new List<RegistradorasResponse>();
        }

        public int PageIndex { get; set; }
        public int TotalItems { get; set; }
        public ICollection<RegistradorasResponse> Registradoras { get; set; }
    }
}
