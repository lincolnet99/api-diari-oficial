using Regulatorio.SharedKernel.Responses;

namespace Regulatorio.Domain.Response.Contatos
{
    public class ObterContatoResponse : BaseResponse
    {
        public ObterContatoResponse()
        {
            Contatos = new List<ContatoResponse>();
        }

        public int PageIndex { get; set; }
        public int TotalItems { get; set; }
        public ICollection<ContatoResponse> Contatos { get; set; }
    }
}
