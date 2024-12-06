using Regulatorio.SharedKernel.Responses;

namespace Regulatorio.Domain.Response.Garantias
{
    public class GarantiaResponse : BaseResponse
    {
        public int Id { get; set; }
        public string Uf { get; set; }
        public string ValorPublico { get; set; }
        public string ValorPrivado { get; set; }
        public string TipoRegistro { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? ModificadoEm { get; set; }
        public string Observacao { get; set; }
        public string ValorTotal { get; set; }
    }
}
