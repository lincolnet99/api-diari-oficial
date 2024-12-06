using Regulatorio.Domain.Entities.Dominios;

namespace Regulatorio.Domain.Repositories.Dominios
{
    public interface IDominioRepository
    {
        Task<ICollection<Dominio>> ObterPorTipoDominio(int tipoDominio);
        Task<ICollection<Dominio>> ObterPorTipoDominio(string tipoDominio);
        Task SalvarTipoDominio(TipoDominio tipoDominio);
        Task Salvar(Dominio dominio);
        Task<ICollection<TipoDominio>> ObterTipoDominio();
        Task<ICollection<Dominio>> ObterDominioPorPalavraChaveTipoDominio(string palavraChaveTipoDominio);
        Task<Dominio> ObterDominioPorId(int dominioId);
    }
}