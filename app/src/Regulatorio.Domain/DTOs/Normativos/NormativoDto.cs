using Newtonsoft.Json;
using Regulatorio.SharedKernel.Entities;

namespace Regulatorio.Domain.DTOs.Normativos
{
    public class NormativoDto : BaseEntity
    {
        public string Uf { get; set; }
        public string NomePortaria { get; set; }
        public string NomeArquivo { get; set; }
        public int TipoNormativo { get; set; }
        public int TipoRegistro { get; set; }
        public bool EhVisaoNacional { get; set; }
        public int Status { get; set; }
        public DateTime? DataVigencia { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? ModificadoEm { get; set; }

        [JsonIgnore]
        public string UrlArquivo { get;set; }
    }
}
