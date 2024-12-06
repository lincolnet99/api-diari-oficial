using Regulatorio.SharedKernel.Responses;

namespace Regulatorio.Domain.Response.Dominios
{
    public sealed class TipoDominioResponse : IResponse
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}
