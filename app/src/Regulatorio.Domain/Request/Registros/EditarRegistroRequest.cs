using Regulatorio.SharedKernel.Requests;

namespace Regulatorio.Domain.Request.Registros
{
    public class EditarRegistroRequest : BaseEntityRequest
    {
        public string? Uf { get; set; }
        public string? TipoRegistro { get; set; }
        public string? ValorPublico { get; set; }
        public string? ValorPrivado { get; set; }
        public string? ValorTotal { get; set; }
        public string? Observacao { get; set; }
    }
}
