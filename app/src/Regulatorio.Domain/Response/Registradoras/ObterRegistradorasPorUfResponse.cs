using Regulatorio.SharedKernel.Responses;

namespace Regulatorio.Domain.Response.Registradoras
{
    public class ObterRegistradorasPorUfResponse : BaseResponse
    {
        public ObterRegistradorasPorUfResponse()
        {
            Registradoras = new List<RegistradorasPorUfResponse>();
        }

        public int PageIndex { get; set; }
        public int TotalItems { get; set; }
        public ICollection<RegistradorasPorUfResponse> Registradoras { get; set; }
    }
}
