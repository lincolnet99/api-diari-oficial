using Regulatorio.SharedKernel.Entities;

namespace Regulatorio.Domain.Entities.Normativos
{
    public class Normativo : BaseEntity
    {
        public int IdUf { get; set; }
        public string NomePortaria { get; set; }
        public string NomeArquivo { get; set; }
        public DateTime DataVigencia { get; set; }
        public int TipoNormativo { get; set; }
        public DateTime DataCriacao { get; set; }
        public int TipoRegistro { get; set; }
        public bool VisaoNacional { get; set; }
        public int Status { get; set; }
    }
}
