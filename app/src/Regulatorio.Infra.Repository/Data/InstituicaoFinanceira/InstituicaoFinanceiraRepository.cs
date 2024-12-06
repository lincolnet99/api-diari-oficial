using Dapper;
using Microsoft.Data.SqlClient;
using Regulatorio.Domain.DTOs.InstituicaoFinanceira;
using Regulatorio.Domain.Repositories.InstituicaoFinanceiraRepository;
using Regulatorio.Domain.Request.InstituicaoFinanceira;
using Regulatorio.SharedKernel.Common;
using System.Text;

namespace Regulatorio.Infra.Repository.Data.InstituicaoFinanceira
{
    public class InstituicaoFinanceiraRepository : BaseRepository, IInstituicaoFinanceiraRepository
    {
        public async Task<int> CriarInstituicaoFinanceira(CriarInstituicaoFinanceiraRequest request)
        {
            await using var conn = new SqlConnection(ConnectionString);

            var sql = new StringBuilder();

            #region SqlQuery

            sql.Append(@"INSERT INTO [InstituicaoFinanceira].[InstituicaoFinanceira]
                               (Uf, 
                                PrecoCadastro, 
                                PrecoRenovacaoCadastro, 
                                Periodicidade, 
                                Observacoes, 
                                CriadoEm)
		                       OUTPUT inserted.Id
                         VALUES
                               (@Uf,
                                @PrecoCadastro,
                                @PrecoRenovacaoCadastro,
                                @Periodicidade,
                                @Observacoes,
                                @CriadoEm)");

            #endregion

            var instituicaoFinanceira = await conn.QueryAsync<int>(sql.ToString(), new
            {
                request.Uf,
                request.PrecoCadastro,
                request.PrecoRenovacaoCadastro,
                request.Periodicidade,
                request.Observacoes,
                CriadoEm = DateTime.Now,
            });

            return instituicaoFinanceira.SingleOrDefault();
        }

        public async Task<InstituicaoFinanceiraDto> EditarInstituicaoFinanceira(EditarInstituicaoFinanceiraRequest request)
        {
            await using var conn = new SqlConnection(ConnectionString);

            var sql = new StringBuilder("UPDATE InstituicaoFinanceira.InstituicaoFinanceira SET ");


            sql.Append("PrecoCadastro = @PrecoCadastro, ");
            sql.Append("PrecoRenovacaoCadastro = @PrecoRenovacaoCadastro, ");
            sql.Append("Uf = @Uf, ");
            sql.Append("Periodicidade = @Periodicidade, ");
            sql.Append("Observacoes = @Observacoes, ");
            sql.Append("ModificadoEm = @ModificadoEm");

            sql.Append(" WHERE Id = @Id");

            await conn.ExecuteAsync(sql.ToString(), new
            {
                Id = request.Id,
                request.Uf,
                request.PrecoCadastro,
                request.PrecoRenovacaoCadastro,
                request.Periodicidade,
                request.Observacoes,
                ModificadoEm = DateTime.Now
            });

            var response = await ObterInstituicaoFinanceiraPorId(request.Id);

            return response;
        }

        public async Task<int> ExcluirInstituicaoFinanceira(int idInstituicaoFinanceira)
        {
            await using var conn = new SqlConnection(ConnectionString);
            await conn.OpenAsync();
            await using var tran = conn.BeginTransaction();
            try
            {

                var sql = new StringBuilder();
                sql.Append(@"DELETE FROM InstituicaoFinanceira.Documentos WHERE IdInstituicaoFinanceira = @idInstituicaoFinanceira");

                await conn.ExecuteAsync(sql.ToString(), new { IdInstituicaoFinanceira = idInstituicaoFinanceira }, tran);

                sql = new StringBuilder();
                sql.Append(@"DELETE FROM InstituicaoFinanceira.InstituicaoFinanceira WHERE Id = @idInstituicaoFinanceira");

                await conn.ExecuteAsync(sql.ToString(), new { IdInstituicaoFinanceira = idInstituicaoFinanceira }, tran);

                await tran.CommitAsync();

            }
            catch (Exception)
            {
                await tran.RollbackAsync();
            }

            return idInstituicaoFinanceira;
        }

        public async Task<int> ExcluirInstituicaoFinanceiraDocumentos(int idInstituicaoFinanceira)
        {
            await using var conn = new SqlConnection(ConnectionString);
            var sql = new StringBuilder();

            sql.Append(@"DELETE FROM InstituicaoFinanceira.Documentos WHERE Id = @idInstituicaoFinanceira");

            var result = await conn.QueryAsync(sql.ToString(), new { IdInstituicaoFinanceira = idInstituicaoFinanceira });

            return idInstituicaoFinanceira;
        }

        public async Task<InstituicaoFinanceiraDto> ObterInstituicaoFinanceiraPorUf(string uf)
        {
            await using var conn = new SqlConnection(ConnectionString);

            var sql = new StringBuilder();

            #region SqlQuery

            sql.Append(@"select 
	                    	a.Id, A.Uf, A.PrecoCadastro, PrecoRenovacaoCadastro, Periodicidade, Observacoes, a.CriadoEm 'DataCriacaoIf', 
	                        B.Id 'IdDocumento', b.Documento, b.IdInstituicaoFinanceira,b.CriadoEm 'DataCriacaoDocumento'
                        from 
	                        InstituicaoFinanceira.InstituicaoFinanceira (NOLOCK) A
	                        LEFT JOIN InstituicaoFinanceira.Documentos (NOLOCK) B
		                        ON A.Id = B.IdInstituicaoFinanceira 
                         WHERE a.Uf = @Uf");

            #endregion

            var dicionario = new Dictionary<int, InstituicaoFinanceiraDto>();
            var query = await conn.QueryAsync<InstituicaoFinanceiraDto, InstituicaoFinanceiraDocumentosDto, InstituicaoFinanceiraDto>(
                sql.ToString(),
                 (pai, filhos) =>
                 {
                     if (pai == null)
                         return pai;

                     if (!dicionario.TryGetValue(pai.Id, out var paiExistente))
                     {
                         paiExistente = pai;
                         paiExistente.Documentos = new List<InstituicaoFinanceiraDocumentosDto>();
                         dicionario[pai.Id] = paiExistente;
                     }

                     if (filhos != null)
                     {
                         paiExistente.Documentos.Add(filhos);
                     }

                     return paiExistente;
                 }, new { Uf = uf }, splitOn: "IdDocumento");

            return dicionario.Values.FirstOrDefault();
        }

        public async Task<InstituicaoFinanceiraDto> ObterInstituicaoFinanceiraPorId(int id)
        {
            await using var conn = new SqlConnection(ConnectionString);

            var sql = new StringBuilder();

            #region SqlQuery

            sql.Append(@"select 
	                    	a.Id, A.Uf, A.PrecoCadastro, PrecoRenovacaoCadastro, Periodicidade, Observacoes, a.CriadoEm 'DataCriacaoIf', 
	                        B.Id 'IdDocumento', b.Documento, b.IdInstituicaoFinanceira,b.CriadoEm 'DataCriacaoDocumento'
                        from 
	                        InstituicaoFinanceira.InstituicaoFinanceira (NOLOCK) A
	                        LEFT JOIN InstituicaoFinanceira.Documentos (NOLOCK) B
		                        ON A.Id = B.IdInstituicaoFinanceira 
                         WHERE a.Id = @Id");

            #endregion

            var dicionario = new Dictionary<int, InstituicaoFinanceiraDto>();
            var query = await conn.QueryAsync<InstituicaoFinanceiraDto, InstituicaoFinanceiraDocumentosDto, InstituicaoFinanceiraDto>(
                sql.ToString(),
                 (pai, filhos) =>
                 {
                     if (pai == null)
                         return pai;

                     if (!dicionario.TryGetValue(pai.Id, out var paiExistente))
                     {
                         paiExistente = pai;
                         paiExistente.Documentos = new List<InstituicaoFinanceiraDocumentosDto>();
                         dicionario[pai.Id] = paiExistente;
                     }

                     if (filhos != null)
                     {
                         paiExistente.Documentos.Add(filhos);
                     }

                     return paiExistente;
                 }, new { Id = id }, splitOn: "IdDocumento");

            return dicionario.Values.FirstOrDefault();

        }

        public async Task<PagedList<InstituicaoFinanceiraDto>> ObterInstituicoesFinanceiras(ObterInstituicoesFinanceirasRequest request)
        {
            await using var conn = new SqlConnection(ConnectionString);
            var pagedResults = new PagedList<InstituicaoFinanceiraDto>();

            var sql = new StringBuilder();

            #region SqlQuery

            sql.Append(@"select 
	                    	a.Id, A.Uf, A.PrecoCadastro, PrecoRenovacaoCadastro, Periodicidade, Observacoes, a.CriadoEm 'DataCriacaoIf', 
	                        B.Id 'IdDocumento', b.Documento, b.IdInstituicaoFinanceira,b.CriadoEm 'DataCriacaoDocumento'
                        from 
	                        InstituicaoFinanceira.InstituicaoFinanceira (NOLOCK) A
	                        LEFT JOIN InstituicaoFinanceira.Documentos (NOLOCK) B
		                        ON A.Id = B.IdInstituicaoFinanceira ");

            if (request.Id > 0)
                sql.Append("WHERE a.Id = @Id");

            if (request.Uf.Length > 0)
                sql.Append(" AND Uf IN @Uf");

            if (string.IsNullOrEmpty(request.Sort))
            {
                sql.Append(@" ORDER BY a.CriadoEm DESC OFFSET @pageSize * @pageIndex ROWS
                                 FETCH NEXT @pageSize ROWS ONLY ");
            }
            else
            {
                var columnName = request.Sort.Split('.')[0];
                var order = request.Sort.Split('.')[1];

                sql.Append(@$" ORDER BY {columnName} {order.ToUpper()} OFFSET @pageSize * @pageIndex ROWS
                                 FETCH NEXT @pageSize ROWS ONLY ");
            }

            sql.AppendLine();

            sql.Append(@"SELECT COUNT(*) FROM InstituicaoFinanceira.InstituicaoFinanceira ");

            if (request.Id > 0)
                sql.Append("WHERE Id = @Id");

            if (request.Uf.Length > 0)
                sql.Append(" AND Uf IN @Uf");

            #endregion

            var query = await conn.QueryMultipleAsync(sql.ToString(), new
            {
                request.Uf,
                request.Id,
                request.PageIndex,
                request.PageSize,
                request.Sort
            });


            var dicionario = new Dictionary<int, InstituicaoFinanceiraDto>();

            var resultados = query.Read<InstituicaoFinanceiraDto, InstituicaoFinanceiraDocumentosDto, InstituicaoFinanceiraDto>(
                (pai, filhos) =>
                {
                    if (pai == null)
                        return pai;

                    if (!dicionario.TryGetValue(pai.Id, out var paiExistente))
                    {
                        paiExistente = pai;
                        paiExistente.Documentos = new List<InstituicaoFinanceiraDocumentosDto>();
                        dicionario[pai.Id] = paiExistente;
                    }

                    if (filhos != null)
                    {
                        paiExistente.Documentos.Add(filhos);
                    }

                    return paiExistente;
                }, splitOn: "IdDocumento");

            pagedResults.Items = dicionario.Values.ToList();

            pagedResults.TotalCount = query.ReadFirstOrDefault<int>();

            return pagedResults;
        }

        public async Task<bool> CriarInstituicaoFinanceiraDocumentos(List<string> documentos, int instituicaoFinanceira)
        {
            try
            {
                await using var conn = new SqlConnection(ConnectionString);

                var sql = new StringBuilder();

                sql.Append("INSERT INTO InstituicaoFinanceira.Documentos (idInstituicaoFinanceira, Documento, CriadoEm)" +
                    " values (@IdInstituicaoFinanceira, @Documento, @CriadoEm)");

                var documentosInsert = new List<dynamic>();

                documentos.ForEach(x => documentosInsert.Add(new { IdInstituicaoFinanceira = instituicaoFinanceira, Documento = x, CriadoEm = DateTime.Now }));

                await conn.ExecuteAsync(sql.ToString(), documentosInsert);

                return true;
            }

            catch (Exception ex)
            {
                return false;
            }


        }

        public async Task<InstituicaoFinanceiraDto> EditarInstituicaoFinanceiraDocumentos(EditarInstituicaoFinanceiraRequest request)
        {
            await using var conn = new SqlConnection(ConnectionString);
            await conn.OpenAsync();
            await using var tran = conn.BeginTransaction();

            var response = new InstituicaoFinanceiraDto();

            try
            {
                var sql = new StringBuilder("DELETE FROM InstituicaoFinanceira.Documentos WHERE IdInstituicaoFinanceira = @Id");

                await conn.ExecuteAsync(sql.ToString(), new
                {
                    Id = request.Id

                }, tran);

                var sqlDocs = new StringBuilder();

                sqlDocs.Append("INSERT INTO InstituicaoFinanceira.Documentos (idInstituicaoFinanceira, Documento, CriadoEm)" +
                    " values (@IdInstituicaoFinanceira, @Documento, @CriadoEm)");

                var documentosInsert = new List<dynamic>();

                request.Documentos.ForEach(x => documentosInsert.Add(new { IdInstituicaoFinanceira = request.Id, Documento = x.Documento, CriadoEm = DateTime.Now }));

                await conn.ExecuteAsync(sqlDocs.ToString(), documentosInsert, tran);

                await tran.CommitAsync();
                response = await ObterInstituicaoFinanceiraPorId(request.Id);

            }
            catch (Exception)
            {
                await tran.RollbackAsync();
            }


            return response;

        }
    }
}
