using Regulatorio.SharedKernel.Requests;

namespace Regulatorio.Domain.Request.Dominios
{
    public sealed class CriarTipoDominioRequest : BaseEntityRequest
    {
        public required string Nome { get; set; }
        public required string Descricao { get; set; }
        public required string Categoria { get; set; }
    }
}
