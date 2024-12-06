using Regulatorio.SharedKernel.Responses;

namespace Regulatorio.Domain.Response.InstituicaoFinanceira
{
    public class InstituicaoFinanceiraResponse : BaseResponse
    {
        public int Id { get; set; }
        public string? Uf { get; set; }
        public string? PrecoCadastro { get; set; }
        public string? PrecoRenovacaoCadastro { get; set; }
        public string? Periodicidade { get; set; }
        public string? Observacoes { get; set; }

        public List<InstituicaoFinanceiraDocumentosResponse> Documentos { get; set; }
    }

    public class InstituicaoFinanceiraDocumentosResponse
    {
        public string Id { get; set; }
        public string Documento { get; set; }
    }
}
