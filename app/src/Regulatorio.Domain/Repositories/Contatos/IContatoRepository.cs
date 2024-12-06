using Regulatorio.Domain.DTOs.Contatos;
using Regulatorio.Domain.Request.Contatos;
using Regulatorio.SharedKernel.Common;

namespace Regulatorio.Domain.Repositories.Contatos
{
    public interface IContatoRepository
    {
        Task<PagedList<ContatoDto>> ObterContatos(ObterContatoRequest request);
        Task<int> CriarContato(CriarContatoRequest request);
        Task<ContatoDto> ObterContatoPorId(int id);
        Task<ContatoDto> EditarContato(int idContato, EditarContatoRequest request);

        Task<ContatoDto> ObterContatoPorUf(string uf);

        Task<int> ExcluirContato(int idContato);

    }
}
