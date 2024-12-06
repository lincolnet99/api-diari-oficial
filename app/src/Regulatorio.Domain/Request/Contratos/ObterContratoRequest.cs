using Regulatorio.SharedKernel.Requests;

namespace Regulatorio.Domain.Request.Contatos
{
    public class ObterContatoRequest : BaseEntityRequest
    {
        public ObterContatoRequest()
        {
            PageIndex = 0;
            PageSize = 10;
        }

        public string[] Uf { get; set; } = Array.Empty<string>();

        public string? Nome { get; set; }
        public string? Orgao { get; set; }

        public string? Cargo { get; set; }

        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public string? Sort { get; set; }
    }
}
