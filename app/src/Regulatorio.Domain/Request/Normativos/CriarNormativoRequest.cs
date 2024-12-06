using Microsoft.AspNetCore.Http;
using Regulatorio.SharedKernel.Requests;

namespace Regulatorio.Domain.Request.Normativos
{
    public class CriarNormativoRequest : BaseEntityRequest
    {
        public required string NomePortaria { get; set; }
        public string? Uf { get; set; }
        public required IFormFile Arquivo { get; set; }
        public DateTime? DataVigencia { get; set; } = null;
        public required int TipoRegistro { get; set; }
        public required int TipoNormativo { get; set; }
        public bool VisaoNacional { get; set; }
    }
}
