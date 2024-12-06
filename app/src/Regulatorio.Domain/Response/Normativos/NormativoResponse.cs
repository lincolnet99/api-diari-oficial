using Regulatorio.SharedKernel.Responses;

namespace Regulatorio.Domain.Response.Normativos
{
    public class NormativoResponse : BaseResponse
    {
        public int Id { get; set; }
        public string Uf { get; set; }
        public string NomePortaria { get; set; }
        public string NomeArquivo { get; set; }
        public DateTime? DataVigencia { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? ModificadoEm { get; set; }
        public int TipoNormativo { get; set; }
        public int TipoRegistro { get; set; }
        public int Status { get; set; }
        public bool EhVisaoNacional { get; set; }
    }
}
