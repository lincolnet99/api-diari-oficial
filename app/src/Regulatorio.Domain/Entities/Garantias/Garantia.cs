using Regulatorio.SharedKernel.Entities;

namespace Regulatorio.Domain.Entities.Garantias
{
    public class Garantia : BaseEntity
    {
        public int IdUf { get; set; }
        public string ValorPublico { get; set; }
        public string ValorPrivado { get; set; }
        public string TipoRegistro { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Observacao { get; set; }
        public string ValorTotal { get; set; }
    }
}
