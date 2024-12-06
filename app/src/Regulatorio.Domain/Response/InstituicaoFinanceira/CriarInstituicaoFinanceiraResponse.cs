using Regulatorio.SharedKernel.Responses;

namespace Regulatorio.Domain.Response.InstituicaoFinanceira
{
    public class CriarInstituicaoFinanceiraResponse : BaseResponse
    {
        public int Id { get; set; }
        public string? Uf { get; set; }
        public string? PrecoCadastro { get; set; }
        public string? PrecoRenovacaoCadastro { get; set; }
        public int Periodicidade { get; set; }
        public string? Observacoes { get; set; }
    }
}
