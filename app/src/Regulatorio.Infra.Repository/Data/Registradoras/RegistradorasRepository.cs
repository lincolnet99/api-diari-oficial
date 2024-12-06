using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Regulatorio.Domain.DTOs.Normativos;
using Regulatorio.Domain.DTOs.Registradora;
using Regulatorio.Domain.Repositories.RegistradoraRepository;
using Regulatorio.Domain.Request.Registradoras;
using Regulatorio.SharedKernel.Common;
using System.Text;

namespace Regulatorio.Infra.Repository.Data.RegistradorasRepository
{
    public class RegistradorasRepository : BaseRepository, IRegistradoraRepository
    {
        public async Task<PagedList<RegistradoraDto>> CriarRegistradora(CriarRegistradoraRequest request)
        {

            await using var conn = new SqlConnection(ConnectionString);
            await conn.OpenAsync();
            await using var tran = conn.BeginTransaction();

            var response = new PagedList<RegistradoraDto>();

            try
            {
                var sqlValidaEmpresa = new StringBuilder("select ID from Registradoras.Empresas where nome = @nome");
                var idValidaEmpresa = await conn.QueryAsync<int>(sqlValidaEmpresa.ToString(), new { nome = request.NomeEmpresa }, tran);

                if (!idValidaEmpresa.Any())
                {
                    var sql = new StringBuilder("INSERT INTO Registradoras.Empresas (Nome, DataCriacao)  OUTPUT INSERTED.Id VALUES (@Nome, @DataCriacao)");

                    idValidaEmpresa = await conn.QueryAsync<int>(sql.ToString(), new
                    {
                        Nome = request.NomeEmpresa,
                        DataCriacao = DateTime.Now

                    }, tran);
                }

                var sqlRegistradora = new StringBuilder();

                sqlRegistradora.Append("INSERT INTO Registradoras.Registradoras (IdEmpresa, Uf, DataInicioVigencia, DataTerminoVigencia, Preco, DataCriacao)" +
                    " OUTPUT INSERTED.Id values (@IdEmpresa, @Uf, @DataInicioVigencia, @DataTerminoVigencia, @Preco, @DataCriacao)");

                //var registradoras = new List<dynamic>();

                //request.ForEach(x => registradoras.Add(new
                //{
                //    Uf = x.Uf,
                //    IdEmpresa = idEmpresa.SingleOrDefault(),
                //    DataInicioVigencia = x.DataInicioVigencia,
                //    DataTerminoVigencia = x.DataTerminoVigencia,
                //    Preco = x.Preco,
                //    DataCriacao = DateTime.Now
                //}));

                await conn.ExecuteAsync(sqlRegistradora.ToString(), new
                {
                    Uf = request.Uf,
                    IdEmpresa = idValidaEmpresa.SingleOrDefault(),
                    DataInicioVigencia = request.DataInicioVigencia,
                    DataTerminoVigencia = request.DataTerminoVigencia,
                    Preco = request.Preco,
                    DataCriacao = DateTime.Now
                }, tran);

                await tran.CommitAsync();

                response = await ObterRegistradoras(new ObterRegistradorasRequest { NomeEmpresa = request.NomeEmpresa });

                return response;

            }
            catch (Exception)
            {
                await tran.RollbackAsync();
                return response;
            }

        }

        public async Task<RegistradoraDto> EditarRegistradora(EditarRegistradoraRequest request, string fileName, string url )
        {
            await using var conn = new SqlConnection(ConnectionString);

            var sql = new StringBuilder("UPDATE Registradoras.Registradoras SET ");

            sql.Append("IdEmpresa = @IdEmpresa, ");
            sql.Append("Uf = @Uf, ");
            sql.Append("Preco = @Preco, ");
            sql.Append("DataInicioVigencia = @DataInicioVigencia, ");
            sql.Append("DataTerminoVigencia= @DataTerminoVigencia, ");
            sql.Append("DataModificacao = @ModificadoEm  ");

            if(!String.IsNullOrEmpty(fileName))
                sql.Append(",Portaria = @fileName ");

            if (!String.IsNullOrEmpty(url))
                sql.Append(",UrlArquivo = @url");


            sql.Append(" WHERE Id = @Id");

            await conn.ExecuteAsync(sql.ToString(), new
            {
                Id = request.Id,
                IdEmpresa = request.IdEmpresa,
                request.Uf,
                request.Preco,
                request.DataInicioVigencia,
                request.DataTerminoVigencia,
                ModificadoEm = DateTime.Now,
                fileName,
                url
                
            });

            var response = await ObterRegistradoraPorId(request.Id);

            return response;
        }

        public async Task<int> ExcluirRegistradora(int idRegistradora)
        {
            await using var conn = new SqlConnection(ConnectionString);
            await conn.OpenAsync();
            await using var tran = conn.BeginTransaction();
            try
            {
                var sql = new StringBuilder();
                sql.Append(@"DELETE FROM Registradoras.Registradoras WHERE Id = @IdRegistradoras");

                await conn.ExecuteAsync(sql.ToString(), new { IdRegistradoras = idRegistradora }, tran);

                await tran.CommitAsync();

            }
            catch (Exception)
            {
                await tran.RollbackAsync();
            }

            return idRegistradora;
        }


        public async Task<PagedList<RegistradoraDto>> ObterRegistradoraPorUf(string uf)
        {
            await using var conn = new SqlConnection(ConnectionString);
            var pagedResults = new PagedList<RegistradoraDto>();

            var sql = new StringBuilder();

            #region SqlQuery

            sql.Append(@"select 
	                    a.Id, 
	                    a.Uf, 
	                    a.DataInicioVigencia, 
	                    a.DataTerminoVigencia, 
	                    a.Preco, 
	                    a.DataCriacao 'DataCriacaoRegistradora', 
	                    a.DataModificacao 'DataModificacaoRegistradora',  
	                    b.Id 'IdEmpresa',
                        a.Portaria,
                        a.UrlArquivo as UrlPortaria,
	                    b.Nome, 
	                    b.DataCriacao 'DataCriacaoEmpresa', 
	                    b.DataModificacao 'DataModificacaoEmpresa'
                    from 
	                    Registradoras.Registradoras (nolock) a
	                    inner join Registradoras.Empresas (nolock) b
		                    on a.IdEmpresa = b.Id where 1 = 1"
            );

            if (uf.Length > 0)
                sql.Append(" AND Uf = @Uf");


            sql.AppendLine();

            sql.Append(@"SELECT COUNT(*) FROM Registradoras.Registradoras ");

            if (uf.Length > 0)
                sql.Append(" WHERE Uf = @Uf");

            #endregion

            var query = await conn.QueryMultipleAsync(sql.ToString(), new
            {
                Uf = uf
            });

            var resultados = query.Read<RegistradoraDto, RegistradoraEmpresaDto, RegistradoraDto>(
                 (registradora, empresa) =>
                 {
                     registradora.Empresas = empresa;
                     return registradora;
                 }, splitOn: "IdEmpresa");

            pagedResults.Items = resultados.ToList();

            pagedResults.TotalCount = query.ReadFirstOrDefault<int>();

            return pagedResults;
        }

        public async Task<RegistradoraDto> ObterRegistradoraPorId(int id)
        {
            await using var conn = new SqlConnection(ConnectionString);

            var sql = new StringBuilder();

            #region SqlQuery

            sql.Append(@"select 
	                    a.Id, 
	                    a.Uf, 
	                    a.DataInicioVigencia, 
	                    a.DataTerminoVigencia, 
	                    a.Preco, 
	                    a.DataCriacao 'DataCriacaoRegistradora', 
	                    a.DataModificacao 'DataModificacaoRegistradora',  
                        a.Portaria,
                        a.UrlArquivo as UrlPortaria,
	                    b.Id 'IdEmpresa',
	                    b.Nome, 
	                    b.DataCriacao 'DataCriacaoEmpresa', 
	                    b.DataModificacao 'DataModificacaoEmpresa'
                    from 
	                    Registradoras.Registradoras (nolock) a
	                    inner join Registradoras.Empresas (nolock) b
		                    on a.IdEmpresa = b.Id where a.Id = @Id");

            #endregion


            var query = await conn.QueryAsync<RegistradoraDto, RegistradoraEmpresaDto, RegistradoraDto>(
                sql.ToString(),
                 (registradora, empresa) =>
                 {
                     registradora.Empresas = empresa;
                     return registradora;
                 }, new
                 {
                     Id = id
                 }, splitOn: "IdEmpresa");

            return query.FirstOrDefault();

        }

        public async Task<PagedList<RegistradoraDto>> ObterRegistradoras(ObterRegistradorasRequest request)
        {
            await using var conn = new SqlConnection(ConnectionString);
            var pagedResults = new PagedList<RegistradoraDto>();

            var sql = new StringBuilder();

            #region SqlQuery

        sql.Append(@"select 
	                    a.Id, 
	                    a.Uf, 
	                    a.DataInicioVigencia, 
	                    a.DataTerminoVigencia, 
	                    a.Preco, 
	                    a.DataCriacao 'DataCriacaoRegistradora', 
	                    a.DataModificacao 'DataModificacaoRegistradora', 
                        a.Portaria,
                        a.UrlArquivo as UrlPortaria,
	                    b.Id 'IdEmpresa',
	                    b.Nome, 
	                    b.DataCriacao 'DataCriacaoEmpresa', 
	                    b.DataModificacao 'DataModificacaoEmpresa'
                    from 
	                    Registradoras.Registradoras (nolock) a
	                    inner join Registradoras.Empresas (nolock) b
		                    on a.IdEmpresa = b.Id where 1 = 1");

            if (request.Id > 0)
                sql.Append(" AND a.Id = @Id");

            if (request.Uf.Length > 0)
                sql.Append(" AND Uf IN @Uf");

            if (!string.IsNullOrEmpty(request.NomeEmpresa))
            sql.Append(" AND b.nome LIKE '%' + @NomeEmpresa + '%'");

            if (string.IsNullOrEmpty(request.Sort))
            {
                sql.Append(@" ORDER BY a.DataCriacao DESC OFFSET @pageSize * @pageIndex ROWS
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

            sql.Append(@"SELECT COUNT(*) FROM Registradoras.Registradoras where 1 = 1");

            if (request.Id > 0)
                sql.Append(" AND Id = @Id");

            if (request.Uf.Length > 0)
                sql.Append(" AND Uf IN @Uf");

            #endregion

            var query = await conn.QueryMultipleAsync(sql.ToString(), new
            {
                Uf = request.Uf.ToList(),
                request.Id,
                request.PageIndex,
                request.PageSize,
                request.Sort,
                request.NomeEmpresa
            });

            var resultados = query.Read<RegistradoraDto, RegistradoraEmpresaDto, RegistradoraDto>(
                 (registradora, empresa) =>
                 {
                     registradora.Empresas = empresa;
                     return registradora;
                 }, splitOn: "IdEmpresa");

            pagedResults.Items = resultados.ToList();

            pagedResults.TotalCount = query.ReadFirstOrDefault<int>();

            return pagedResults;
        }

        public async Task<PagedList<RegistradoraDto>> ObterRegistradoraPorEmpresa(string empresa)
        {
            await using var conn = new SqlConnection(ConnectionString);
            var pagedResults = new PagedList<RegistradoraDto>();

            var sql = new StringBuilder();

            #region SqlQuery

            sql.Append(@"select 
	                    a.Id, 
	                    a.Uf, 
	                    a.DataInicioVigencia, 
	                    a.DataTerminoVigencia, 
	                    a.Preco, 
	                    a.DataCriacao 'DataCriacaoRegistradora', 
	                    a.DataModificacao 'DataModificacaoRegistradora',  
                        a.Portaria,
                        a.UrlArquivo as UrlPortaria,
	                    b.Id 'IdEmpresa',
	                    b.Nome, 
	                    b.DataCriacao 'DataCriacaoEmpresa', 
	                    b.DataModificacao 'DataModificacaoEmpresa'
                    from 
	                    Registradoras.Registradoras (nolock) a
	                    inner join Registradoras.Empresas (nolock) b
		                    on a.IdEmpresa = b.Id where 1 = 1"
            );

            if (empresa.Length > 0)
                sql.Append(" AND b.nome LIKE '%' + @Empresa + '%'");


            sql.AppendLine();

            #endregion

            var query = await conn.QueryMultipleAsync(sql.ToString(), new
            {
                Empresa = empresa
            });

            var resultados = query.Read<RegistradoraDto, RegistradoraEmpresaDto, RegistradoraDto>(
                 (registradora, empresa) =>
                 {
                     registradora.Empresas = empresa;
                     return registradora;
                 }, splitOn: "IdEmpresa");

            pagedResults.Items = resultados.ToList();

            pagedResults.TotalCount = resultados.Count();

            return pagedResults;
        }

        public async Task<int> EditarArquivoRegistradora(string urlArquivo, int id, string fileName)
        {
            await using var conn = new SqlConnection(ConnectionString);

            const string sql = @"UPDATE Registradoras.Registradoras
                                 SET UrlArquivo = @UrlArquivo, Portaria = @Portaria
                                 WHERE Id = @IdRegistradora";

            await conn.ExecuteAsync(sql, new
            {
                IdRegistradora = id,
                UrlArquivo = urlArquivo,
                Portaria = fileName
            });

            return id;
        }


        public async Task<DownloadRegistratoraDto> ObterRegistradoraParaDownload(int id)
        {
            await using var conn = new SqlConnection(ConnectionString);

            const string sql = @"SELECT Id,
		                                portaria NomeArquivo,
		                                UrlArquivo
		                                FROM Registradoras.Registradoras WHERE Id = @id";

            var result = await conn.QueryAsync<DownloadRegistratoraDto>(sql, new {id});

            return result.SingleOrDefault();
        }


    }
}
