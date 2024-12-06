using Regulatorio.SharedKernel.Responses;

namespace Regulatorio.Domain.Response.Normativos
{
    public class ObterUfsRecentesResponse : BaseResponse
    {
        public ObterUfsRecentesResponse()
        {
            Ufs = new List<string>();
        }

        public ICollection<string> Ufs { get; set; }
    }
}
