using Regulatorio.SharedKernel.Entities;

namespace Regulatorio.Domain.DTOs.Normativos
{
    public class DownloadNormativoDto : BaseEntity
    {
        public string NomeArquivo { get; set; }
        public string UrlArquivo { get; set; }
    }
}
