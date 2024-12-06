using Microsoft.AspNetCore.Http;
using Regulatorio.SharedKernel.Requests;

namespace Regulatorio.Domain.Request.Normativos
{
    public class EditarNormativoRequest : BaseEntityRequest
    {
        public string? Uf { get; set; }
        public string? NomePortaria { get; set; }
        public IFormFile? Arquivo { get; set; }
        public DateTime? DataVigencia { get; set; } = null;
        public int? TipoRegistro { get; set; }
        public int? TipoNormativo { get; set; }
        public bool? VisaoNacional { get; set; }
    }
}
