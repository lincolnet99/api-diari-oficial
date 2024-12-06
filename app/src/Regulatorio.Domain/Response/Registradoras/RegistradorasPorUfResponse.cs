using Regulatorio.SharedKernel.Responses;

namespace Regulatorio.Domain.Response.Registradoras
{
    public class RegistradorasPorUfResponse : BaseResponse
    {
        public string? Empresa { get; set; }
        public string? Ufs { get; set; }


    }
}
