using Regulatorio.SharedKernel.Responses;

namespace Regulatorio.Domain.Response.Normativos
{
    public class AtivarArquivarNormativoResponse : BaseResponse
    {
        public int Id { get; set; }
        public int StatusAtual { get; set; }
    }
}
