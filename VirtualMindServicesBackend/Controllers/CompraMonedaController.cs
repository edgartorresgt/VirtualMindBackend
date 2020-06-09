using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VirtualMindServicesBackend.Dtos;
using VirtualMindServicesBackend.Helper;
using VirtualMindServicesBackend.Interfaces;

namespace VirtualMindServicesBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraMonedaController : ControllerBase
    {
        private readonly ITransaccionMoneda _transaccionMoneda;
        private readonly ILogger<CompraMonedaController> _logger;

        public CompraMonedaController(ITransaccionMoneda transaccionMoneda, ILogger<CompraMonedaController> logger)
        {
            _transaccionMoneda = transaccionMoneda;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post(TransaccionDtoRequest transaccionDtoRequest)
        {
            var resultadoValidacion = Extensiones.MonedaValida(transaccionDtoRequest.MonedaCompra);
            if (!string.IsNullOrWhiteSpace(resultadoValidacion))
            {
                _logger.LogError(resultadoValidacion);
                return BadRequest(resultadoValidacion);
            }
            if (await _transaccionMoneda.GenerarTransaccion(transaccionDtoRequest))
                return Ok();

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var transacciones = await _transaccionMoneda.GetTransacciones();
            return Ok(transacciones);
        }
    }
}