using Microsoft.AspNetCore.Mvc;
using TrabajoFinalRestaurante.Calendario;
using TrabajoFinalRestaurante.Services.Interfaces;

namespace TrabajoFinalRestaurante.Controllers
{

    [ApiController]
    [Route("api/calendario")]
    public class CalendarioController : ControllerBase
    {
        private readonly ICalendarioService _calendarioService;

        public CalendarioController(ICalendarioService calendarioService)
        {
            _calendarioService = calendarioService;
        }

        [HttpGet]
        public async Task<ActionResult<CalendarioResponse>> ObtenerCalendario()
        {
            var calendario = await _calendarioService.GetCalendarioAsync();
            return Ok(calendario);
        }
    }
}
