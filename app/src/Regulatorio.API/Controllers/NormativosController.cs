using Microsoft.AspNetCore.Mvc;
using Regulatorio.API.Infrastructure.Controllers;
using Regulatorio.Domain.Request.Normativos;
using Regulatorio.Domain.Services.Normativos;

namespace Regulatorio.API.Controllers
{
    [Route("api/normativos")]
    [ApiController]
    public class NormativosController : BaseController
    {
        private readonly INormativoService _normativoService;

        public NormativosController(INormativoService normativoService)
        {
            _normativoService = normativoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterListaNormativos([FromQuery] ObterListaNormativosRequest request)
        {
           var response = await _normativoService.ObterNormativos(request);

            if (response.IsSuccess)
                return Ok(200, response);

            return Error(401, response.Errors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterNormativoPorId(int id)
        {
            var response = await _normativoService.ObterNormativoPorId(id);

            if (response.IsSuccess)
                return Ok(200, response);

            return Error(401, response.Errors);
        }

        [HttpPost]
        public async Task<IActionResult> CriarNormativo([FromForm] CriarNormativoRequest request)
        {
            var response = await _normativoService.CriarNormativo(request);

            if (response.IsSuccess)
                return Ok(200, response);

            return Error(401, response.Errors);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AlterarNormativo(int id, [FromForm] EditarNormativoRequest request)
        {
            var response = await _normativoService.EditarNormativo(id, request);

            if (response.IsSuccess)
                return Ok(200, response);

            return Error(401, response.Errors);
        }

        [HttpPut("ativar-arquivar/{id}")]
        public async Task<IActionResult> AtivarArquivarNormativo(int id)
        {
            var response = await _normativoService.AtivarArquivarNormativo(id);

            if (response.IsSuccess)
                return Ok(200, response);

            return Error(401, response.Errors);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirNormativo(int id)
        {
            var response = await _normativoService.ExcluirNormativo(id);

            if (response.IsSuccess)
                return Ok(200, response);

            return Error(401, response.Errors);
        }

        [HttpGet("download/{id}")]
        public async Task<IActionResult> DownloadNormativo(int id)
        {
            var response = await _normativoService.ObterNormativoParaDownload(id);

            if (response.IsSuccess)
            {
                return File(response.Arquivo.ToArray(), "application/octet-stream", response.NomeArquivo);
            }

            return Error(401, response.Errors);
        }

        [HttpGet("ufs-recentes")]
        public async Task<IActionResult> ListarUfRecentes()
        {
            var response = await _normativoService.ObterUfsRecentes();

            if (response.IsSuccess)
                return Ok(200, response);

            return Error(401, response.Errors);
        }
    }
}
