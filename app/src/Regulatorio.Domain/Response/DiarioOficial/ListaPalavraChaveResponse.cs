using Regulatorio.Domain.DTOs.PalavrasChave;
using Regulatorio.SharedKernel.Responses;

namespace Regulatorio.Domain.Response.PalavrasChave
{
    public class ListaPalavraChaveResponse : BaseResponse
    {
        public List<PalavraChaveResponse> ListaPalavras { get; set; }
    }

    public class PalavraChaveResponse
    {
        public int Id { get; set; }
		public string Palavra { get; set; }
		public string CriadaPor { get; set; }
		public DateTime CriadaEm { get; set; }
		public int Ativa { get; set; }
	}
}
