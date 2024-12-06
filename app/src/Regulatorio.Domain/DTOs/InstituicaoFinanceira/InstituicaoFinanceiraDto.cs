using Regulatorio.SharedKernel.Entities;

namespace Regulatorio.Domain.DTOs.InstituicaoFinanceira
{
    public class InstituicaoFinanceiraDto : BaseEntity
    {
        public int Id { get; set; }
        public string? Uf { get; set; }
        public string? PrecoCadastro { get; set; }
        public string? PrecoRenovacaoCadastro { get; set; }
        public string? Periodicidade { get; set; }
        public string? Observacoes { get; set; }
        public DateTime DataCriacaoIf { get; set; }
        public List<InstituicaoFinanceiraDocumentosDto> Documentos { get; set; }
    }
}
