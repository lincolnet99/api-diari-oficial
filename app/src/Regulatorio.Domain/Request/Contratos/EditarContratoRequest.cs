using Regulatorio.SharedKernel.Requests;

namespace Regulatorio.Domain.Request.Contatos
{
    public class EditarContatoRequest : BaseEntityRequest
    {
        public string Uf { get; set; }
        public string? Orgao { get; set; }
        public string? Cargo { get; set; }
        public string Nome { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
    }
}
