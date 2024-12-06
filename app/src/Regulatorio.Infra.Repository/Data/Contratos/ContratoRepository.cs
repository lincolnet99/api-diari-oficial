using Azure.Core;
using Dapper;
using Microsoft.Data.SqlClient;
using Regulatorio.Domain.DTOs.Contatos;
using Regulatorio.Domain.DTOs.Registros;
using Regulatorio.Domain.Repositories.Contatos;
using Regulatorio.Domain.Request.Contatos;
using Regulatorio.Domain.Request.Registros;
using Regulatorio.SharedKernel.Common;
using System.Text;

namespace Regulatorio.Infra.Repository.Data.Registros
{
    public class ContatoRepository : BaseRepository, IContatoRepository
    {

        public async Task<PagedList<ContatoDto>> ObterContatos(ObterContatoRequest request)
        {
            await using var conn = new SqlConnection(ConnectionString);
            var pagedResults = new PagedList<ContatoDto>();

            var sql = new StringBuilder();

            #region SqlQuery

            sql.Append(@"SELECT Id 
                                ,Uf
                                ,Orgao
                                ,Cargo
                                ,Nome
                                ,Telefone
                                ,Email
                          FROM Contatos.Contatos WHERE 1 = 1");

            if (request.Uf.Length > 0)
                sql.Append(" AND Uf IN @Uf");

            if (!string.IsNullOrEmpty(request.Nome))
                sql.Append(" AND Nome LIKE '%' + TRIM(@Nome) + '%'");

            if (!string.IsNullOrEmpty(request.Orgao))
                sql.Append(" AND Orgao LIKE '%' + TRIM(@Orgao) + '%'");

             if (!string.IsNullOrEmpty(request.Cargo))
                sql.Append(" AND Cargo LIKE '%' + TRIM(@Cargo) + '%'");



            if (string.IsNullOrEmpty(request.Sort))
            {
                sql.Append(@" ORDER BY UF DESC OFFSET @pageSize * @pageIndex ROWS
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

            sql.Append(@"SELECT COUNT(*) FROM Contatos.Contatos WHERE 1 = 1");

            if (request.Uf.Length > 0)
                sql.Append(" AND Uf IN @Uf");

            #endregion

            var registros = await conn.QueryMultipleAsync(sql.ToString(), new
            {
                Uf = request.Uf.ToList(),
                request.Nome,
                request.Orgao,
                request.Cargo,
                request.PageIndex,
                request.PageSize,
                request.Sort
            });

            pagedResults.Items = registros.Read<ContatoDto>();
            pagedResults.TotalCount = registros.ReadFirstOrDefault<int>();

            return pagedResults;
        }

        public async Task<int> CriarContato(CriarContatoRequest request)
        {
            await using var conn = new SqlConnection(ConnectionString);

            var sql = new StringBuilder();

            #region SqlQuery

            sql.Append(@"INSERT INTO [Contatos].[Contatos]
                                   ([Uf]
                                   ,[Orgao]
                                   ,[Cargo]
                                   ,[Nome]
                                   ,[Telefone]
                                   ,[Email])
		                       OUTPUT inserted.Id
                         VALUES
                               (
                                @Uf,
                                @Orgao,
                                @Cargo,
                                @Nome,
                                @Telefone,
                                @Email)"
                    );

            #endregion

            var registro = await conn.QueryAsync<int>(sql.ToString(), new
            {
                request.Uf,
                request.Orgao,
                request.Cargo,
                request.Nome,
                request.Telefone,
                request.Email,
                CriadoEm = DateTime.Now,
            });

            return registro.SingleOrDefault();
        }


        public async Task<ContatoDto> EditarContato(int idContato, EditarContatoRequest request)
        {
            await using var conn = new SqlConnection(ConnectionString);

            var sql = new StringBuilder();

            sql.Append(@"UPDATE Contatos.Contatos SET
                                        Uf = @Uf,
                                        Orgao = @Orgao,
                                        Cargo = @Cargo, 
                                        Nome = @Nome,
                                        Telefone = @Telefone"

            );

            sql.Append(" WHERE Id = @Id");

            await conn.ExecuteAsync(sql.ToString(), new
            {
                Id = idContato,
                request.Uf,
                request.Orgao,
                request.Cargo,
                request.Nome,
                request.Telefone,
                ModificadoEm = DateTime.Now
            });

            var response = await ObterContatoPorId(idContato);

            return response;
        }


        public async Task<ContatoDto> ObterContatoPorId(int id)
        {
            await using var conn = new SqlConnection(ConnectionString);

            var sql = new StringBuilder();

            #region SqlQuery

            sql.Append(@"SELECT  Id 
                                ,Uf
                                ,Orgao
                                ,Cargo
                                ,Nome
                                ,Telefone
                                ,Email
                          FROM Contatos.Contatos WHERE Id = @Id");

            #endregion

            var garantia = await conn.QueryAsync<ContatoDto>(sql.ToString(), new
            {
                Id = id
            });

            return garantia.SingleOrDefault();
        }

        public async Task<ContatoDto> ObterContatoPorUf(string uf)
        {
            await using var conn = new SqlConnection(ConnectionString);

            var sql = new StringBuilder();

            #region SqlQuery

            sql.Append(@"SELECT  Id 
                                ,Uf
                                ,Orgao
                                ,Cargo
                                ,Nome
                                ,Telefone
                                ,Email
                          FROM Contatos.Contatos WHERE Uf = @Uf");

            #endregion

            var garantia = await conn.QueryAsync<ContatoDto>(sql.ToString(), new
            {
                Uf = uf
            });

            return garantia.FirstOrDefault();
        }


        public async Task<int> ExcluirContato(int idContato)
        {
            await using var conn = new SqlConnection(ConnectionString);
            await conn.OpenAsync();
            await using var tran = conn.BeginTransaction();
            try
            {
                var sql = new StringBuilder();
                sql.Append(@"DELETE FROM Contatos.Contatos WHERE Id = @IdContato");

                await conn.ExecuteAsync(sql.ToString(), new { IdContato = idContato }, tran);

                await tran.CommitAsync();

            }
            catch (Exception)
            {
                await tran.RollbackAsync();
            }

            return idContato;
        }


    }
}
