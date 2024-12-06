using Regulatorio.SharedKernel.Entities;

namespace Regulatorio.Domain.Entities.Dominios
{
    public class TipoDominio : BaseEntity
    {
        public required string Nome { get; set; }
        public required string Descricao { get; set; }
        public required string Categoria { get; set; }
        public DateTime CriadoEm { get; } = DateTime.Now;
    }
}