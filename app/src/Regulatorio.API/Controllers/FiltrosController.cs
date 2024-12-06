using Microsoft.AspNetCore.Mvc;
using Regulatorio.API.Infrastructure.Controllers;
using Regulatorio.Domain.Services.Filtros;

namespace Regulatorio.API.Controllers
{
    [Route("api/filtros")]
    [ApiController]
    public class FiltrosController : BaseController
    {
        private readonly IFiltroService _filtroService;

        public FiltrosController(IFiltroService filtroService)
        {
            _filtroService = filtroService;
        }

        [HttpGet("tipo-normativo")]
        public async Task<IActionResult> ObterTipoNormativo()
        {
            var response = await _filtroService.ObterTipoNormativo();

            if (response.IsSuccess)
                return Ok(200, response);

            return Error(401, response.Errors);
        }
    }
}
