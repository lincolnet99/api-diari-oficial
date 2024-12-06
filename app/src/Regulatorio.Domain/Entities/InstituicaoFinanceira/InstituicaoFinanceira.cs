using Regulatorio.SharedKernel.Entities;

namespace Regulatorio.Domain.Entities.InscricaoEstatual
{
    public class InstituicaoFinanceira : BaseEntity
    {
        public string? Uf { get; set; }
        public decimal PrecoCadastro { get; set; }
        public decimal PrecoRenovacaoCadastro { get; set; }
        public int Periodicidade { get; set; }
        public string? Observacoes { get; set; }

    }
}
