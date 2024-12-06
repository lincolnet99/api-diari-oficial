using Regulatorio.SharedKernel.Entities;

namespace Regulatorio.Domain.Entities.Registros
{
    public class Registro : BaseEntity
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
