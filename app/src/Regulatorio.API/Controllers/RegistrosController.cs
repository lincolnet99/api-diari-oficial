using Microsoft.AspNetCore.Mvc;
using Regulatorio.API.Infrastructure.Controllers;
using Regulatorio.Domain.Request.Registros;
using Regulatorio.Domain.Services.Registros;

namespace Regulatorio.API.Controllers
{
    [Route("api/registros")]
    [ApiController]
    public class RegistrosController : BaseController
    {
        private readonly IRegistroService _registroService;

        public RegistrosController(IRegistroService registroService)
        {
            _registroService = registroService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterRegistros([FromQuery] ObterRegistrosRequest request)
        {
            var response = await _registroService.ObterRegistros(request);

            if (response.IsSuccess)
                return Ok(200, response);

            return Error(401, response.Errors);
        }

        [HttpGet("{uf}")]
        public async Task<IActionResult> ObterRegistroPorUf(string uf)
        {
            var response = await _registroService.ObterRegistroPorUf(uf);

            if (response.IsSuccess)
                return Ok(200, response);

            return Error(401, response.Errors);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObterRegistroPorId(int id)
        {
            var response = await _registroService.ObterRegistroPorId(id);

            if (response.IsSuccess)
                return Ok(200, response);

            return Error(401, response.Errors);
        }

        [HttpPost]
        public async Task<IActionResult> CriarRegistro([FromBody] CriarRegistroRequest request)
        {
            var response = await _registroService.CriarRegistro(request);

            if (response.IsSuccess)
                return Ok(200, response);

            return Error(401, response.Errors);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarRegistro(int id, [FromBody] EditarRegistroRequest request)
        {
            var response = await _registroService.EditarRegistro(id, request);

            if (response.IsSuccess)
                return Ok(200, response);

            return Error(401, response.Errors);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirRegistro(int id)
        {
            var response = await _registroService.ExcluirRegistro(id);

            if (response.IsSuccess)
                return Ok(200, response);

            return Error(401, response.Errors);
        }
    }
}
