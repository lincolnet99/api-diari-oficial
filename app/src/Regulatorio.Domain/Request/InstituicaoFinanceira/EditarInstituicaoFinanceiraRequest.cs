using Regulatorio.SharedKernel.Requests;

namespace Regulatorio.Domain.Request.InstituicaoFinanceira
{
    public class EditarInstituicaoFinanceiraRequest : BaseEntityRequest
    {
        public int Id { get; set; }
        public string Uf { get; set; }
        public string? PrecoCadastro { get; set; }
        public string? PrecoRenovacaoCadastro { get; set; }
        public string? Periodicidade { get; set; }
        public string? Observacoes { get; set; }
        public List<EditarInstituicaoFinanceiraDocumentosRequest> Documentos { get; set; }
    }

    public class EditarInstituicaoFinanceiraDocumentosRequest
    {
        public string Documento { get; set; }

    }
}
