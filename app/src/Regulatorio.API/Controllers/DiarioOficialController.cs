using Microsoft.AspNetCore.Mvc;
using Regulatorio.API.Infrastructure.Controllers;
using Regulatorio.Domain.Request.DiarioOficial;
using Regulatorio.Domain.Request.PalavrasChave;
using Regulatorio.Domain.Services.PalavrasChave;

namespace Regulatorio.API.Controllers
{
    [Route("api/diario-oficial")]
    [ApiController]
    public class DiarioOficialController : BaseController
    {
        private readonly IDiarioOficialService _diarioOficialService;

        public DiarioOficialController(IDiarioOficialService palavrasChaveService)
        {
            _diarioOficialService = palavrasChaveService;
        }

        [HttpGet("/api/consultarpalavraschave")]
        public async Task<IActionResult> ObterListaPalavrasChave()
        {
            var response = await _diarioOficialService.ObterListaPalavrasChave();

            return Ok(200, response);
        }

        [HttpPost("/api/criarpalavrachave")]
        public async Task<IActionResult> CriarPalavraChave([FromBody] CriarPalavraChaveRequest request)
        {
            var response = await _diarioOficialService.CriarPalavraChave(request);
            return Ok(200, response);
        }

        [HttpDelete("/api/excluirpalavrachave/{id}")]
        public async Task<IActionResult> ExcluirPalavraChave(int id)
        {
            var response = await _diarioOficialService.ExcluirPalavraChave(id);
            return Ok(200, response);
        }

        [HttpGet("/api/arquivosprocessados")]
        public async Task<IActionResult> ObterListaArquivosProcessados([FromQuery] ConsultarListagemArquivosProcessadosRequest request)
        {
            var response = await _diarioOficialService.ObterListaArquivosProcessados(request);

            return Ok(200, response);
        }

        [HttpGet("/api/palavraschavesrelevantes")]
        public async Task<IActionResult> ObterListaPalavrasChavesRelevantes()
        {
            var response = await _diarioOficialService.PalavrasChavesRelevantes();

            return Ok(200, response);
        }

        [HttpGet("/api/obterultimosregistrosporUF")]
        public async Task<IActionResult> ObterUltimosRegistrosPorUF()
        {
            var response = await _diarioOficialService.ObterUltimosRegistrosPorUF();

            return Ok(200, response);
        }
        
        [HttpGet("/api/obterestatisticas")]
        public async Task<IActionResult> ObterEstatisticas()
        {
            var response = await _diarioOficialService.ObterEstatisticas();

            return Ok(200, response);
        }
    }
}
