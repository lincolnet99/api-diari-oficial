using Regulatorio.SharedKernel.Requests;

namespace Regulatorio.Domain.Request.InstituicaoFinanceira
{
    public class CriarInstituicaoFinanceiraRequest : BaseEntityRequest
    {
        public string? Uf { get; set; }
        public string? PrecoCadastro { get; set; }
        public string? PrecoRenovacaoCadastro { get; set; }
        public string? Periodicidade { get; set; }
        public string? Observacoes { get; set; }

        public List<string> Documentos { get; set; }
    }

}
