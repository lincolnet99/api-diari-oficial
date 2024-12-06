using Regulatorio.SharedKernel.Responses;

namespace Regulatorio.Domain.Response.Dominios
{
    public sealed class DominioResponse : BaseResponse
    {
        public DominioResponse()
        {
            ValorDominio = new List<ValorDominio>();
        }

        public string TipoDominio { get; set; }
        public ICollection<ValorDominio> ValorDominio { get; set; }
    }

    public class ValorDominio
    {
        public int Id { get; set; }
        public string PalavraChave { get; set; }
        public string Valor { get; set; }
    }
}
