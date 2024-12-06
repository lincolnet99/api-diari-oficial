using Regulatorio.SharedKernel.Entities;

namespace Regulatorio.Domain.Entities.InscricaoEstatual
{
    public class InstituicaoFinanceiraDocumentos : BaseEntity
    {
        public string Documento { get; set; }
        public int IdInstituicaoFinanceira { get; set; }
    }
}
