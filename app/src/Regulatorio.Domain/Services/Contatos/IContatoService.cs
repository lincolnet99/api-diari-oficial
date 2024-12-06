using Regulatorio.Domain.Request.Contatos;
using Regulatorio.Domain.Response.Contatos;

namespace Regulatorio.Domain.Services.Contatos
{
    public interface IContatoService : IDisposable
    {
        Task<ObterContatoResponse> ObterContatos(ObterContatoRequest request);
        Task<CriarContatoResponse> CriarContato(CriarContatoRequest request);
        Task<ContatoResponse> EditarContato(int idContato, EditarContatoRequest request);

        Task<ExcluirContatoResponse> ExcluirContato(int idContato);

    }
}
