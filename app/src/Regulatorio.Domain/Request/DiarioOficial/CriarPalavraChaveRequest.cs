using Regulatorio.SharedKernel.Requests;

namespace Regulatorio.Domain.Request.PalavrasChave
{
    public class CriarPalavraChaveRequest : BaseEntityRequest
    {
        public string Palavra { get; set; }
        public string CriadaPor { get; set; }
    }
}
