using Dapper;
using Microsoft.Data.SqlClient;
using Regulatorio.Domain.DTOs.Normativos;
using Regulatorio.Domain.Enum.Normativos;
using Regulatorio.Domain.Repositories.Normativos;
using Regulatorio.Domain.Request.Normativos;
using Regulatorio.SharedKernel.Common;
using System.Text;

namespace Regulatorio.Infra.Repository.Data.Normativos
{
    public class NormativoRepository : BaseRepository, INormativoRepository
    {
        public async Task<AtivarArquivarNormativoDto> AtivarArquivarNormativo(int IdNormativo)
        {
            await using var conn = new SqlConnection(ConnectionString);

            const string sql = @"UPDATE Normativo.Normativo SET Status = IIF(Status=1,2,1) OUTPUT INSERTED.Id, INSERTED.Status WHERE Id = @NormativoId";

            var result = await conn.QueryAsync<AtivarArquivarNormativoDto>(sql, new { NormativoId = IdNormativo });

            return result.SingleOrDefault();
        }

        public async Task<int> CriarNormativo(CriarNormativoRequest request)
        {
            await using var conn = new SqlConnection(ConnectionString);

            const string sql = @"INSERT INTO [Normativo].[Normativo]
                                                                ([Uf]
                                                                ,[NomePortaria]
                                                                ,[NomeArquivo]
                                                                ,[UrlArquivo]
                                                                ,[TipoNormativo]
                                                                ,[TipoRegistro]
                                                                ,[EhVisaoNacional]
                                                                ,[Status]
                                                                ,[DataVigencia]
                                                                ,[CriadoEm]
                                                                ,[ModificadoEm])
                                                            OUTPUT INSERTED.ID
                                                            VALUES
                                                                (@Uf,
                                                                 @NomePortaria,
                                                                 @NomeArquivo,
                                                                 NULL,
                                                                 @TipoNormativo,
                                                                 @TipoRegistro,
                                                                 @VisaoNacional,
                                                                 @Status,
                                                                 @DataVigencia,
                                                                 @CriadoEm,
                                                                 NULL)";

            var result = await conn.QueryAsync<int>(sql, new
            {
                request.Uf,
                request.NomePortaria,
                NomeArquivo = request.Arquivo.FileName,
                request.TipoNormativo,
                CriadoEm = DateTime.Now,
                request.TipoRegistro,
                request.VisaoNacional,
                Status = (int)StatusNormativoEnum.ATIVO,
                request.DataVigencia
            });

            int response = result.SingleOrDefault();

            return response;
        }

        public async Task<NormativoDto> EditarNormativo(int idNormativo, EditarNormativoRequest request)
        {
            await using var conn = new SqlConnection(ConnectionString);

            var sql = new StringBuilder("UPDATE Normativo.Normativo SET ");

            if (!string.IsNullOrEmpty(request.NomePortaria))
            {
                sql.Append("NomePortaria = @NomePortaria, ");
            }

            if (request.VisaoNacional.HasValue)
            {
                sql.Append("EhVisaoNacional = @VisaoNacional, ");
            }

            if (request.TipoNormativo != null)
            {
                sql.Append("TipoNormativo = @TipoNormativo, ");
            }

            if (request.TipoRegistro != null)
            {
                sql.Append("TipoRegistro = @TipoRegistro, ");
            }

            if (!string.IsNullOrEmpty(request.Uf))
            {
                sql.Append("Uf = @Uf, ");
            }

            if (request.DataVigencia != null)
            {
                sql.Append("DataVigencia = @DataVigencia, ");
            }

            if (request.Arquivo != null)
            {
                sql.Append("NomeArquivo = @NomeArquivo, ");
            }

            sql.Append("ModificadoEm = @ModificadoEm");

            if (sql.ToString()[^2..].ToString() == ", ")
            {
                sql.Remove(sql.Length - 2, 2);
            }

            sql.Append(" WHERE Id = @Id");

            await conn.ExecuteAsync(sql.ToString(), new
            {
                Id = idNormativo,
                request.NomePortaria,
                request.TipoNormativo,
                request.TipoRegistro,
                request.DataVigencia,
                request.Uf,
                request.VisaoNacional,
                NomeArquivo = request.Arquivo?.FileName,
                ModificadoEm = DateTime.Now
            });

            var response = await ObterNormativoPorId(idNormativo);

            return response;
        }

        public async Task<int> ExcluirNormativo(int idNormativo)
        {
            await using var conn = new SqlConnection(ConnectionString);

            string sql = @"DELETE Normativo.Normativo WHERE Id = @NormativoId";

            var result = await conn.QueryAsync(sql, new { NormativoId = idNormativo });

            return idNormativo;
        }

        public async Task<NormativoDto> ObterNormativoPorId(int normativoId)
        {
            await using var conn = new SqlConnection(ConnectionString);

            const string sql = @"SELECT Id,
		                            Uf,
		                            NomePortaria,
		                            NomeArquivo,
		                            TipoNormativo,
		                            TipoRegistro,
		                            EhVisaoNacional,
		                            Status,
		                            DataVigencia,
                                    CriadoEm,
                                    ModificadoEm,
                                    UrlArquivo
		                            FROM Normativo.Normativo WHERE Id = @NormativoId ";

            var result = await conn.QueryAsync<NormativoDto>(sql, new { NormativoId = normativoId });

            return result.SingleOrDefault();
        }

        public async Task<PagedList<NormativoDto>> ObterNormativos(ObterListaNormativosRequest request)
        {
            await using var conn = new SqlConnection(ConnectionString);
            var pagedResults = new PagedList<NormativoDto>();

            var sql = new StringBuilder();

            #region SqlQuery

            sql.Append(@"SELECT [Id]
                                ,[Uf]
                                ,[NomePortaria]
                                ,[NomeArquivo]
                                ,[UrlArquivo]
                                ,[TipoNormativo]
                                ,[TipoRegistro]
                                ,[EhVisaoNacional]
                                ,[Status]
                                ,[DataVigencia]
                                ,[CriadoEm]
                                ,[ModificadoEm]
		                        FROM Normativo.Normativo WHERE 1 = 1");

            //if (request.Uf.Length > 0)
            //{
            //    var ufsFiltro = Helper.TransformarArrayEmString(request.Uf, true);
            //    sql.Append($" AND Uf IN ( {ufsFiltro} )");
            //}

            if (request.Uf.Length > 0)
                sql.Append(" AND Uf IN @Uf");

            if (!string.IsNullOrEmpty(request.NomePortaria))
                sql.Append(" AND NomePortaria LIKE '%' + TRIM(@NomePortaria) + '%'");

            if (!string.IsNullOrEmpty(request.NomeArquivo))
                sql.Append(" AND NomeArquivo LIKE '%' + TRIM(@NomeArquivo) + '%'");

            if (request.Status.Length > 0)
                sql.Append(" AND Status IN @Status");

            if (request.TipoRegistro.Length > 0)
                sql.Append(" AND TipoRegistro IN @TipoRegistro");

            if (request.Tipo.Length > 0)
                sql.Append(" AND TipoNormativo IN @Tipo");

            if (request.VisaoNacional.HasValue)
                sql.Append(" AND EhVisaoNacional = @VisaoNacional");

            if (request.DataInicioCriacao != null && request.DataFimCriacao != null)
                sql.Append(" AND CriadoEm BETWEEN @DataInicioCriacao AND @DataFimCriacao");

            if (request.DataInicioVigencia != null && request.DataFimVigencia != null)
                sql.Append(" AND DataVigencia BETWEEN @DataInicioVigencia AND @DataFimVigencia");

            if (string.IsNullOrEmpty(request.Sort))
            {
                sql.Append(@" ORDER BY CriadoEm DESC OFFSET @pageSize * @pageIndex ROWS
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

            sql.Append(@"SELECT COUNT(*) FROM Normativo.Normativo WHERE 1 = 1");

            if (request.Uf.Length > 0)
                sql.Append(" AND Uf IN @Uf");

            if (!string.IsNullOrEmpty(request.NomePortaria))
                sql.Append(" AND NomePortaria LIKE '%' + TRIM(@NomePortaria) + '%'");

            if (!string.IsNullOrEmpty(request.NomeArquivo))
                sql.Append(" AND NomeArquivo LIKE '%' + TRIM(@NomeArquivo) + '%'");

            if (request.Status.Length > 0)
                sql.Append(" AND Status IN @Status");

            if (request.TipoRegistro.Length > 0)
                sql.Append(" AND TipoRegistro IN @TipoRegistro");

            if (request.Tipo.Length > 0)
                sql.Append(" AND TipoNormativo IN @Tipo");

            if (request.VisaoNacional.HasValue)
                sql.Append(" AND EhVisaoNacional = @VisaoNacional");

            if (request.DataInicioCriacao != null && request.DataFimCriacao != null)
                sql.Append(" AND CriadoEm BETWEEN @DataInicioCriacao AND @DataFimCriacao");

            if (request.DataInicioVigencia != null && request.DataFimVigencia != null)
                sql.Append(" AND DataVigencia BETWEEN @DataInicioVigencia AND @DataFimVigencia");

            #endregion

            var normativos = await conn.QueryMultipleAsync(sql.ToString(), new
            {
                request.Uf,
                request.NomePortaria,
                request.NomeArquivo,
                request.DataInicioVigencia,
                request.DataFimVigencia,
                request.DataInicioCriacao,
                request.DataFimCriacao,
                request.Status,
                request.TipoRegistro,
                request.Tipo,
                request.VisaoNacional,
                request.PageIndex,
                request.PageSize,
                request.Sort
            });

            pagedResults.Items = normativos.Read<NormativoDto>();
            pagedResults.TotalCount = normativos.ReadFirstOrDefault<int>();

            return pagedResults;
        }

        public async Task<DownloadNormativoDto> ObterNormativoParaDownload(int idNormativo)
        {
            await using var conn = new SqlConnection(ConnectionString);

            const string sql = @"SELECT Id,
		                                NomeArquivo,
		                                UrlArquivo
		                                FROM Normativo.Normativo WHERE Id = @NormativoId";

            var result = await conn.QueryAsync<DownloadNormativoDto>(sql, new
            {
                NormativoId = idNormativo,
            });

            return result.SingleOrDefault();
        }

        public async Task<IEnumerable<UfsRecentesDto>> ObterUfsRecentes()
        {
            await using var conn = new SqlConnection(ConnectionString);

            const string sql = @"SELECT DISTINCT Uf FROM Normativo.Normativo
                                WHERE (CriadoEm >= DATEADD(DAY, -5, GETDATE()) OR ModificadoEm >= DATEADD(DAY, -5, GETDATE()))
                                UNION
                                SELECT DISTINCT Uf FROM Registro.Contrato
                                WHERE (CriadoEm >= DATEADD(DAY, -5, GETDATE()) OR ModificadoEm >= DATEADD(DAY, -5, GETDATE()))
                                UNION
                                SELECT DISTINCT Uf FROM Registro.Garantia
                                WHERE (CriadoEm >= DATEADD(DAY, -5, GETDATE()) OR ModificadoEm >= DATEADD(DAY, -5, GETDATE()));";

            var result = await conn.QueryAsync<UfsRecentesDto>(sql);

            return result;
        }

        public async Task<int> EditarArquivoNormativo(string urlArquivo, int id)
        {
            await using var conn = new SqlConnection(ConnectionString);

            const string sql = @"UPDATE Normativo.Normativo
                                 SET UrlArquivo = @UrlArquivo
                                 WHERE Id = @IdNormativo";

            await conn.ExecuteAsync(sql, new
            {
                IdNormativo = id,
                UrlArquivo = urlArquivo
            });

            return id;
        }
    }
}
