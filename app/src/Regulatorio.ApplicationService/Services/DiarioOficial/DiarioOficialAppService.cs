using Regulatorio.Domain.DTOs.PalavrasChave;
using Regulatorio.Domain.Repositories.PalavrasChave;
using Regulatorio.Domain.Request.DiarioOficial;
using Regulatorio.Domain.Request.PalavrasChave;
using Regulatorio.Domain.Response.Contatos;
using Regulatorio.Domain.Response.DiarioOficial;
using Regulatorio.Domain.Response.PalavrasChave;
using Regulatorio.Domain.Services.PalavrasChave;
using Regulatorio.SharedKernel.Mappers;
using Regulatorio.SharedKernel.Services;

namespace Regulatorio.ApplicationService.Services.PalavrasChave
{
    public class DiarioOficialAppService : BaseService, IDiarioOficialService
    {
        private readonly IDiarioOficialRepository _diarioOficialRepository;

        public DiarioOficialAppService(IDiarioOficialRepository diarioOficialRepository)
        {
            _diarioOficialRepository = diarioOficialRepository;
        }

        public async Task<ListaPalavraChaveResponse> ObterListaPalavrasChave()
        {
            var response = new ListaPalavraChaveResponse() { ListaPalavras = new List<PalavraChaveResponse>() };
            var retornoPalavras = await _diarioOficialRepository.ObterPalavrasChave();

            foreach (var palavra in retornoPalavras) {
                response.ListaPalavras.Add(new PalavraChaveResponse()
                {
                    Id = palavra.Id,
                    Palavra = palavra.Palavra,
					CriadaPor = palavra.CriadaPor,
                    CriadaEm = palavra.CriadaEm,
                    Ativa = palavra.Ativa
				});
            }

            return response;
        }

        public async Task<int> CriarPalavraChave(CriarPalavraChaveRequest request)
        {
            return await _diarioOficialRepository.IncluirPalavraChave(request);
        }

        public async Task<int> ExcluirPalavraChave(int palavraChaveId)
        {
            return await _diarioOficialRepository.ApagarPalavraChave(palavraChaveId);
        }

        public async Task<ListaArquivosProcessadosResponse> ObterListaArquivosProcessados(ConsultarListagemArquivosProcessadosRequest request)
        {
            var response = new ListaArquivosProcessadosResponse() { ListaArquivoProcessado = new List<ArquivoProcessadoResponse>() };
            var retornoProcessados = await _diarioOficialRepository.ObterArquivosProcessados(request);

            foreach (var item in retornoProcessados.Items)
                response.ListaArquivoProcessado.Add(item.ToResponse<ArquivoProcessadoResponse>());

            response.PageIndex = request.PageIndex;
            response.TotalItems = retornoProcessados.TotalCount;

            return response;
        }

        public async Task<PalavrasChavesRelevantes> PalavrasChavesRelevantes()
        {
            return await _diarioOficialRepository.ObterPalavrasChaveRelevantes();
        }
        
        public async Task<List<ArquivoUFData>> ObterUltimosRegistrosPorUF()
        {
            return await _diarioOficialRepository.ObterUltimosRegistrosPorUF();
        }
        public async Task<DadosDocumentos> ObterEstatisticas()
        {
            return await _diarioOficialRepository.ObterEstatisticas();
        }
    }
}
