using Regulatorio.SharedKernel.Responses;

namespace Regulatorio.Domain.Response.Normativos
{
    public class ObterNormativoParaDownloadResponse : BaseResponse
    {
        public int Id { get; set; }
        public string NomeArquivo { get; set; }
        public MemoryStream Arquivo { get; set; }
    }
}
