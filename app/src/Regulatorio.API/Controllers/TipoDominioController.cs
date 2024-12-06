using Microsoft.AspNetCore.Mvc;
using Regulatorio.API.Infrastructure.Controllers;
using Regulatorio.Domain.Services.Dominios;

namespace Regulatorio.API.Controllers
{
    [Route("api/tipo-dominios")]
    [ApiController]
    public class TipoDominioController : BaseController
    {
        private readonly IDominioService _dominioService;

        public TipoDominioController(IDominioService dominioService)
        {
            _dominioService = dominioService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTipoDominio()
        {
            var response = await _dominioService.ObterTipoDominio(Guid.NewGuid());

            if (response.IsSuccess)
                return Ok(200, response);

            return Error(401, response.Errors);
        }

        [HttpGet("{tipoDominio}/dominios")]
        public async Task<IActionResult> ObterDominio([FromRoute] string tipoDominio)
        {
            var response = await _dominioService.ObterDominioPorTipo(Guid.NewGuid(), tipoDominio).ConfigureAwait(false);

            if (response.IsSuccess)
                return Ok(200, response);

            return Error(401, response.Errors);
        }

        //TODO
        //Cadastrar Tipo normativo, Tipo Registro, Ufs e novas legendas
    }
}