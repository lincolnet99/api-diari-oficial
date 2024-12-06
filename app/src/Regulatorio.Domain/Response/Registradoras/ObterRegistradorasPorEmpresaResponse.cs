using Regulatorio.SharedKernel.Responses;

namespace Regulatorio.Domain.Response.Registradoras
{
    public class ObterRegistradorasPorEmpresaResponse : BaseResponse
    {
        public ObterRegistradorasPorEmpresaResponse()
        {
            Registradoras = new List<RegistradorasPorEmpresaResponse>();
        }

        public int PageIndex { get; set; }
        public int TotalItems { get; set; }
        public ICollection<RegistradorasPorEmpresaResponse> Registradoras { get; set; }
    }
}
