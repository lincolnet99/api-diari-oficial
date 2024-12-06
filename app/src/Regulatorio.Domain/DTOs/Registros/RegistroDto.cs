using Regulatorio.SharedKernel.Entities;

namespace Regulatorio.Domain.DTOs.Registros
{
    public class RegistroDto : BaseEntity
    {
        public string Uf { get; set; }
        public string ValorPublico { get; set; }
        public string ValorPrivado { get; set; }
        public string TipoRegistro { get; set; }
        public string Observacao { get; set; }
        public string ValorTotal { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime ModificadoEm { get; set; }
    }
}
