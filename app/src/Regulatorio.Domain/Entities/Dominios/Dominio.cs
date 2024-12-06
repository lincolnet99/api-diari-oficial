using Regulatorio.Domain.Enum.Dominios;
using Regulatorio.SharedKernel.Entities;

namespace Regulatorio.Domain.Entities.Dominios
{
    public class Dominio : BaseEntity
    {
        public required string PalavraChave { get; set; }
        public required string Valor { get; set; }
        public bool Ativo { get; set; } = true;
        public TipoDominioEnum TipoDominio { get; set; }
        public DateTime CriadoEm { get; } = DateTime.Now;
    }
}