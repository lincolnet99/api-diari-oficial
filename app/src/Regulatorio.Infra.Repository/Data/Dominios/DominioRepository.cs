using Dapper;
using Microsoft.Data.SqlClient;
using Regulatorio.Domain.Entities.Dominios;
using Regulatorio.Domain.Repositories.Dominios;

namespace Regulatorio.Infra.Repository.Data.Dominios
{
    public class DominioRepository : BaseRepository, IDominioRepository
    {
        public async Task<ICollection<Dominio>> ObterPorTipoDominio(int tipoDominio)
        {
            await using var conn = new SqlConnection(ConnectionString);

            const string sql = @"SELECT d.Id, 
                                        d.PalavraChave,
                                        d.Valor
                                 FROM Dominio.Dominio AS d with(nolock)
                                    INNER JOIN Dominio.TipoDominio AS td with(nolock)
                                        ON d.TipoDominioId = td.Id
                                    WHERE td.Id = @tipoDominio 
                                        AND d.Ativo = 1";

            var dominio = await conn.QueryAsync<Dominio>(sql, new { tipoDominio });

            return dominio.ToList();
        }

        public async Task<ICollection<Dominio>> ObterPorTipoDominio(string tipoDominio)
        {
            await using var conn = new SqlConnection(ConnectionString);

            const string sql = @"SELECT d.Id, 
                                        d.PalavraChave,
                                        d.Valor
                                 FROM Dominio.Dominio AS d with(nolock)
                                    INNER JOIN Dominio.TipoDominio AS td with(nolock)
                                        ON d.TipoDominioId = td.Id
                                    WHERE UPPER(td.Nome) = @tipoDominio 
                                        AND d.Ativo = 1";

            var dominio = await conn.QueryAsync<Dominio>(sql, new { tipoDominio });

            return dominio.ToList();
        }

        public async Task SalvarTipoDominio(TipoDominio tipoDominio)
        {
            await using var conn = new SqlConnection(ConnectionString);

            const string sql = @"IF NOT EXISTS(SELECT * FROM Dominio.TipoDominio WHERE Nome = @Nome and Descricao = @Descricao and Categoria = @Categoria) 
                                 BEGIN
	                                 INSERT INTO Dominio.TipoDominio (Nome, Descricao, Categoria, CriadoEm) VALUES (@Nome, @Descricao, @Categoria, @CriadoEm)
                                 END";

            await conn.ExecuteAsync(sql,
                new
                {
                    tipoDominio.Nome,
                    tipoDominio.Descricao,
                    tipoDominio.Categoria,
                    tipoDominio.CriadoEm
                });
        }

        public async Task Salvar(Dominio dominio)
        {
            await using var conn = new SqlConnection(ConnectionString);

            const string sql = @"IF NOT EXISTS(SELECT * FROM Dominio.Dominio WHERE PalavraChave = @PalavraChave and TipoDominioId = @TipoDominioId) 
                                 BEGIN
	                                 INSERT INTO Dominio.Dominio (PalavraChave, Valor, Ativo, TipoDominioId, CriadoEm) VALUES (@PalavraChave, @Valor, @Ativo, @TipoDominioId, @CriadoEm)
                                 END";

            await conn.ExecuteAsync(sql,
                new
                {
                    dominio.PalavraChave,
                    dominio.Valor,
                    dominio.Ativo,
                    dominio.TipoDominio,
                    dominio.CriadoEm
                });
        }

        public async Task<ICollection<TipoDominio>> ObterTipoDominio()
        {
            await using var conn = new SqlConnection(ConnectionString);

            const string sql = @"SELECT Id,
                                        Nome,
                                        Descricao,
                                        Categoria
                                 FROM Dominio.TipoDominio with(nolock) ORDER BY Categoria, Nome ASC";

            var result = await conn.QueryAsync<TipoDominio>(sql);

            return result.ToList();
        }

        public async Task<ICollection<Dominio>> ObterDominioPorPalavraChaveTipoDominio(string palavraChaveTipoDominio)
        {
            await using var conn = new SqlConnection(ConnectionString);

            const string sql = @"SELECT d.Id,
                                        d.PalavraChave, 
                                        d.Valor,
                                        d.Ativo,
										d.TipoDominioId,
                                        d.CriadoEm
                                 FROM Dominio.TipoDominio td with(nolock)
                                 INNER JOIN Dominio.Dominio d with(nolock) ON d.TipoDominioId = td.Id
                                    WHERE UPPER(td.Nome) = UPPER(@palavraChaveTipoDominio) ";

            var tipoDominio = await conn.QueryAsync<Dominio>(sql, new { palavraChaveTipoDominio });

            return tipoDominio.ToList();
        }

        public async Task<Dominio> ObterDominioPorId(int dominioId)
        {
            await using var conn = new SqlConnection(ConnectionString);

            const string sql = @"SELECT d.Id,
                                        d.PalavraChave, 
                                        d.Valor,
                                        d.Ativo,
										d.TipoDominioId,
                                        d.CriadoEm
                                 FROM Dominio.Dominio d with(nolock)
                                    WHERE d.Id = @dominioId ";

            var dominio = await conn.QueryAsync<Dominio>(sql, new { dominioId });

            return dominio.SingleOrDefault();
        }
    }
}