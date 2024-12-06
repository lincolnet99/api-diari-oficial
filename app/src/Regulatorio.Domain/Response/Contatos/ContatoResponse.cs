using Regulatorio.SharedKernel.Responses;

namespace Regulatorio.Domain.Response.Contatos
{
    public class ContatoResponse : BaseResponse
    {
        public int Id { get; set; }
        public string Uf { get; set; }
        public string? Orgao { get; set; }
        public string? Cargo { get; set; }
        public string Nome { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
    }
}
