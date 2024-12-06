using Dapper;
using Microsoft.Data.SqlClient;
using Regulatorio.Domain.DTOs.Contatos;
using Regulatorio.Domain.DTOs.DiarioOficial;
using Regulatorio.Domain.DTOs.PalavrasChave;
using Regulatorio.Domain.Enum.Normativos;
using Regulatorio.Domain.Repositories.PalavrasChave;
using Regulatorio.Domain.Request.DiarioOficial;
using Regulatorio.Domain.Request.PalavrasChave;
using Regulatorio.Domain.Response.DiarioOficial;
using Regulatorio.SharedKernel.Common;
using System.Linq;
using System.Text;

namespace Regulatorio.Infra.Repository.Data.PalavrasChave
{
    public class DiarioOficialRepository : BaseRepository, IDiarioOficialRepository
    {
        public async Task<List<PalavraChaveDto>> ObterPalavrasChave()
        {
            await using var conn = new SqlConnection(ConnectionString);

            const string sql = @"SELECT    Id
                                          ,Palavra
                                          ,CriadaPor
                                          ,CriadaEm
                                          ,Ativa
                                  FROM PalavraChave";

            var result = await conn.QueryAsync<PalavraChaveDto>(sql);

            return result.ToList();
        }

        public async Task<int> IncluirPalavraChave(CriarPalavraChaveRequest request)
        {
            await using var conn = new SqlConnection(ConnectionString);

            const string sql = @"INSERT INTO PalavraChave
                                       ([Palavra]
                                       ,[CriadaPor]
                                       ,[CriadaEm]
                                       ,[Ativa])
                                 OUTPUT INSERTED.ID
                                 VALUES
                                       (@palavra
                                       ,@criadaPor
                                       ,@criadaEm
                                       ,@ativa)
                            ";

            var result = await conn.QueryAsync<int>(sql, new
            {
                request.Palavra,
                request.CriadaPor,
                CriadaEm = DateTime.Now,
                Ativa = 1,
            });

            int response = result.SingleOrDefault();

            return response;
        }

        public async Task<int> ApagarPalavraChave(int palavraChaveId)
        {
            await using var conn = new SqlConnection(ConnectionString);

            const string sql = @"DELETE FROM [PalavraChave]
                                 WHERE Id = @palavraChaveId";

            var result = await conn.QueryAsync(sql, new { palavraChaveId });

            return palavraChaveId;
        }
        public async Task<PagedList<ArquivoProcessadoDto>> ObterArquivosProcessados(ConsultarListagemArquivosProcessadosRequest request)
        {
            await using var conn = new SqlConnection(ConnectionString);
            var sql = new StringBuilder();
            var pagedResults = new PagedList<ArquivoProcessadoDto>();


            if (request.DataInicial.HasValue)
                request.DataInicial = request.DataInicial.Value.Date;
            if (request.DataFinal.HasValue)
                request.DataFinal = request.DataFinal.Value.Date.AddDays(1).AddMilliseconds(-1);

            sql.Append(@"SELECT    Id
                             ,LinkPagina
                             ,PalavraChave
                             ,NomeArquivo
                             ,ResumoArquivo
                             ,DataArquivo
                             ,LinkDownload
                             ,Estado
                             ,Relevante
                      FROM ArquivosObtidos WHERE 1 = 1");

            if (request.Estado.Length > 0)
                sql.Append(" AND Estado IN @Estado");

            if (request.DataInicial != null && request.DataFinal != null)
                sql.Append(" AND DataArquivo BETWEEN @DataInicial AND @DataFinal");

            if (request.Relevante != null)
                sql.Append(" AND Relevante = @Relevante");

            if (string.IsNullOrEmpty(request.Sort))
            {
                sql.Append(@" ORDER BY Estado DESC OFFSET @pageSize * @pageIndex ROWS
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

            sql.Append(@"SELECT COUNT(*) FROM ArquivosObtidos WHERE 1 = 1");

            var registros = await conn.QueryMultipleAsync(sql.ToString(), new
            {
                Estado = request.Estado.ToList(),
                request.DataInicial,
                request.DataFinal,
                request.Relevante,
                request.PageIndex,
                request.PageSize,
                request.Sort
            });

            pagedResults.Items = registros.Read<ArquivoProcessadoDto>();
            pagedResults.TotalCount = registros.ReadFirstOrDefault<int>();

            return pagedResults;
        }
        public async Task<PalavrasChavesRelevantes> ObterPalavrasChaveRelevantes()
        {
            await using var conn = new SqlConnection(ConnectionString);

            const string sql = @"
        SELECT TOP 5
            PalavraChave AS Palavra,
            COUNT(*) AS Quantidade
        FROM ArquivosObtidos
        WHERE DataArquivo >= @DataLimite
        GROUP BY PalavraChave
        ORDER BY Quantidade DESC";

            var dataLimite = DateTime.Now.AddDays(-30);

            // Mapeia a consulta para a classe PalavraChave
            var arquivos = await conn.QueryAsync<PalavraChave>(sql, new { DataLimite = dataLimite });

            return new PalavrasChavesRelevantes
            {
                ListaPalavras = arquivos.ToList()
            };
        }

        public async Task<List<ArquivoUFData>> ObterUltimosRegistrosPorUF()
        {
            await using var conn = new SqlConnection(ConnectionString);

            const string sql = @"
    WITH UltimosRegistros AS (
        SELECT 
            Estado,
            DataArquivo,
            LinkPagina,
            ROW_NUMBER() OVER (PARTITION BY Estado ORDER BY DataArquivo DESC) AS RowNum
        FROM ArquivosObtidos
    )
    SELECT 
        Estado,
        DataArquivo,
        LinkPagina
    FROM UltimosRegistros
    WHERE RowNum = 1";

            var registros = await conn.QueryAsync<ArquivoUFData>(sql);

            return registros.ToList();
        }

        public async Task<DadosDocumentos> ObterEstatisticas()
        {
            await using var conn = new SqlConnection(ConnectionString);

            const string sql = @"
            SELECT COUNT(*) AS TotalDocumentos
            FROM ArquivosObtidos
            WHERE DataArquivo >= @DataLimite;

            WITH PalavraChaveContagem AS (
                SELECT 
                    Estado,
                    PalavraChave,
                    COUNT(*) AS Contagem,
                    ROW_NUMBER() OVER (PARTITION BY Estado ORDER BY COUNT(*) DESC) AS RowNum
                FROM ArquivosObtidos
                GROUP BY Estado, PalavraChave
            ),
            TotalPorEstado AS (
                SELECT 
                    Estado,
                    COUNT(*) AS Contagem
                FROM ArquivosObtidos
                GROUP BY Estado
            )
            SELECT 
                t.Estado,
                p.PalavraChave,
                t.Contagem
            FROM TotalPorEstado t
            LEFT JOIN PalavraChaveContagem p
                ON t.Estado = p.Estado AND p.RowNum = 1;";

            var dataLimite = DateTime.Now.AddDays(-30);

            using var multi = await conn.QueryMultipleAsync(sql, new { DataLimite = dataLimite });

            var totalDocumentos = await multi.ReadSingleAsync<int>();
            var palavrasChave = (await multi.ReadAsync<PalavraChavePorEstado>()).ToList();

            return new DadosDocumentos
            {
                TotalDocumentos = totalDocumentos,
                PalavrasChavePorEstado = palavrasChave
            };
        }
    }
}
