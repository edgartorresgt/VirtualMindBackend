using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VirtualMindServicesBackend.Helper;
using VirtualMindServicesBackend.Interfaces;

namespace VirtualMindServicesBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CotizacionMonedaController : ControllerBase
    {
        private readonly ILogger<CotizacionMonedaController> _logger;
        private readonly ICotizacionMoneda _cotizacionMoneda;

        public CotizacionMonedaController(ICotizacionMoneda cotizacionMoneda, ILogger<CotizacionMonedaController> logger)
        {
            _logger = logger;
            _cotizacionMoneda = cotizacionMoneda;
        }

        [HttpGet("{moneda}")]
        public async Task<IActionResult> GetCotizacion(string moneda)
        {
            var resultadoValidacion = Extensiones.MonedaValida(moneda);
            if (!string.IsNullOrWhiteSpace(resultadoValidacion))
            {
                _logger.LogError(resultadoValidacion);
                throw new Exception(resultadoValidacion);
            }

            var cotizacion = await _cotizacionMoneda.GetCotizacion(moneda);
            if (cotizacion == null)
                return NoContent();

            return Ok(cotizacion);
        }
    }
}