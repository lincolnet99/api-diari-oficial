using Regulatorio.SharedKernel.Entities;

namespace Regulatorio.Domain.DTOs.Registradora
{
    public class DownloadRegistratoraDto : BaseEntity
    {
        public string NomeArquivo { get; set; }
        public string UrlArquivo { get; set; }
    }
}
