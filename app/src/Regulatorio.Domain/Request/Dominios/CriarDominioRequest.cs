using Regulatorio.SharedKernel.Requests;

namespace Regulatorio.Domain.Request.Dominios
{
    public sealed class CriarDominioRequest : BaseEntityRequest
    {
        public string PalavraChave { get; set; }
        public string Valor { get; set; }
        public bool Ativo { get; set; }
        public int TipoDominioId { get; set; }
    }
}
