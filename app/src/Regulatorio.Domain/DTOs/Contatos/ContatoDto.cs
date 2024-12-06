using Regulatorio.SharedKernel.Entities;

namespace Regulatorio.Domain.DTOs.Contatos
{
    public class ContatoDto : BaseEntity
    {
        public string Uf { get; set; }
        public string? Orgao { get; set; }
        public string? Cargo { get; set; }
        public string Nome { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
    }
}