using Regulatorio.SharedKernel.Responses;

namespace Regulatorio.Domain.Response.Registradoras
{
    public class ObterRegistradoraParaDownloadResponse : BaseResponse
    {
        public int Id { get; set; }
        public string NomeArquivo { get; set; }
        public MemoryStream Arquivo { get; set; }
    }
}
