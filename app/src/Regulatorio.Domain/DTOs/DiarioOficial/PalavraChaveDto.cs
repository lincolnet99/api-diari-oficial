using Regulatorio.SharedKernel.Entities;

namespace Regulatorio.Domain.DTOs.PalavrasChave
{
    public class PalavraChaveDto : BaseEntity
    {
        public string Palavra { get; set; }
        public string CriadaPor { get; set; }
        public DateTime CriadaEm { get; set; }
        public int Ativa { get; set; }
    }
}
