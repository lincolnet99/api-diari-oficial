using Regulatorio.SharedKernel.Entities;

namespace Regulatorio.Domain.Entities.Filtros
{
    public class TipoNormativo : BaseEntity
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime CriadoEm { get; set; }
    }
}
