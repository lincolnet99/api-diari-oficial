using Regulatorio.SharedKernel.Responses;

namespace Regulatorio.Domain.Response.Registros
{
    public class ObterRegistrosResponse : BaseResponse
    {
        public ObterRegistrosResponse()
        {
            Registros = new List<RegistroResponse>();
        }

        public int PageIndex { get; set; }
        public int TotalItems { get; set; }
        public ICollection<RegistroResponse> Registros { get; set; }
    }
}
