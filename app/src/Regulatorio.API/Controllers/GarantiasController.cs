using Microsoft.AspNetCore.Mvc;
using Regulatorio.API.Infrastructure.Controllers;
using Regulatorio.Domain.Request.Garantias;
using Regulatorio.Domain.Services.Garantias;

namespace Regulatorio.API.Controllers
{
    [Route("api/garantias")]
    [ApiController]
    public class GarantiasController : BaseController
    {
        private readonly IGarantiaService _garantiaService;

        public GarantiasController(IGarantiaService garantiaService)
        {
            _garantiaService = garantiaService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterGarantias([FromQuery] ObterGarantiasRequest request)
        {
            var response = await _garantiaService.ObterGarantias(request);

            if (response.IsSuccess)
                return Ok(200, response);

            return Error(401, response.Errors);
        }

        [HttpGet("{uf}")]
        public async Task<IActionResult> ObterGarantiaPorUf(string uf)
        {
            var response = await _garantiaService.ObterGarantiaPorUf(uf);

            if (response.IsSuccess)
                return Ok(200, response);

            return Error(401, response.Errors);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObterRegistroPorId(int id)
        {
            var response = await _garantiaService.ObterGarantiaPorId(id);

            if (response.IsSuccess)
                return Ok(200, response);

            return Error(401, response.Errors);
        }

        [HttpPost]
        public async Task<IActionResult> CriarGarantia([FromBody] CriarGarantiaRequest request)
        {
            var response = await _garantiaService.CriarGarantia(request);

            if (response.IsSuccess)
                return Ok(200, response);

            return Error(401, response.Errors);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AlterarGarantia(int id, [FromBody] EditarGarantiaRequest request)
        {
            var response = await _garantiaService.EditarGarantia(id, request);

            if (response.IsSuccess)
                return Ok(200, response);

            return Error(401, response.Errors);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirGarantia(int id)
        {
            var response = await _garantiaService.ExcluirGarantia(id);

            if (response.IsSuccess)
                return Ok(200, response);

            return Error(401, response.Errors);
        }
    }
}
