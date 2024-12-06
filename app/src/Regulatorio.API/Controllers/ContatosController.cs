using Microsoft.AspNetCore.Mvc;
using Regulatorio.API.Infrastructure.Controllers;
using Regulatorio.Domain.Request.Contatos;
using Regulatorio.Domain.Request.Registros;
using Regulatorio.Domain.Services.Contatos;
using Regulatorio.Domain.Services.Registradoras;
using Regulatorio.Domain.Services.Registros;

namespace Regulatorio.API.Controllers
{
    [Route("api/contatos")]
    [ApiController]
    public class ContatosController : BaseController
    {
        private readonly IContatoService _ContatoService;

        public ContatosController(IContatoService ContatoService)
        {
            _ContatoService = ContatoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterContato([FromQuery] ObterContatoRequest request)
        {
            var response = await _ContatoService.ObterContatos(request);

            if (response.IsSuccess)
                return Ok(200, response);

            return Error(401, response.Errors);
        }

        [HttpPost]
        public async Task<IActionResult> CriarContato([FromBody] CriarContatoRequest request)
        {
            var response = await _ContatoService.CriarContato(request);

            if (response.IsSuccess)
                return Ok(200, response);

            return Error(401, response.Errors);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarContato(int id, [FromBody] EditarContatoRequest request)
        {
            var response = await _ContatoService.EditarContato(id, request);

            if (response.IsSuccess)
                return Ok(200, response);

            return Error(401, response.Errors);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirContato(int id)
        {
            var response = await _ContatoService.ExcluirContato(id);

            if (response.IsSuccess)
                return Ok(200, response);

            return Error(401, response.Errors);
        }
    }
}
