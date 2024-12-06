namespace Regulatorio.Domain.DTOs.InstituicaoFinanceira
{
    public class InstituicaoFinanceiraDocumentosDto
    {
        public int IdDocumento { get; set; }
        public string Documento { get; set; }
        public int IdInstituicaoFinanceira { get; set; }
        public DateTime DataCriacaoDocumento { get; set; }
    }
}
