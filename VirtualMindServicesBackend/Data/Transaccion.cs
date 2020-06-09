using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VirtualMindServicesBackend.Controllers;
using VirtualMindServicesBackend.Dtos;
using VirtualMindServicesBackend.Interfaces;

namespace VirtualMindServicesBackend.Data
{
    public class Transaccion:ITransaccionMoneda
    {
        private readonly DataContext _context;
        private const decimal MontoMaximoMesDolar = 200;
        private const decimal MontoMaximoMesReal = 300;
        private readonly ICotizacionMoneda _cotizacionMoneda;
        private readonly ILogger<CompraMonedaController> _logger;

        public Transaccion(DataContext context, ICotizacionMoneda cotizacionMoneda,  ILogger<CompraMonedaController> logger)
        {
            _context = context;
            _cotizacionMoneda = cotizacionMoneda;
            _logger = logger;
        }

        public async Task<bool> GenerarTransaccion(TransaccionDtoRequest transaccionDtoRequest)
        {
            var result =false;
            var primerDiaMesActual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var ultimoDiaMesActual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));

            var montoMensualAcumulado  = _context.Transaccion.Where(x => x.IdUsuario.Equals(transaccionDtoRequest.IdUsuario) && 
                                                                          x.MonedaCompra.Equals(transaccionDtoRequest.MonedaCompra) && 
                                                                          x.FechaTransaccion >= primerDiaMesActual && x.FechaTransaccion <= ultimoDiaMesActual)
                                                                     .Sum(x => x.MontoCompra);

           var cotizacionMoneda = await _cotizacionMoneda.GetCotizacion(transaccionDtoRequest.MonedaCompra); 

            var montoCompra = Math.Round(transaccionDtoRequest.MontoPesosArgentinos / cotizacionMoneda.CambioVenta, 2);
            montoMensualAcumulado += montoCompra;

            var montoSobrePasaElTotalMes = false;

            switch (transaccionDtoRequest.MonedaCompra.ToLower())
            {
                case "dolar":
                {
                    if (montoMensualAcumulado > MontoMaximoMesDolar)
                        montoSobrePasaElTotalMes = true;
                    _logger.LogError("El monto para comprar dolares ha superado el maximo mensual");
                    break;
                }
                case "real":
                {
                    if (montoMensualAcumulado > MontoMaximoMesReal)
                        montoSobrePasaElTotalMes = true;
                    _logger.LogError("El monto para comprar reales ha superado el maximo mensual");
                        break;
                }
            }

            if (!montoSobrePasaElTotalMes)
            {
                var nuevaTransaccion = new TransaccionDto()
                {
                    IdTransaccion = Guid.NewGuid(),
                    IdUsuario = transaccionDtoRequest.IdUsuario,
                    MontoPesosArgentinos = transaccionDtoRequest.MontoPesosArgentinos,
                    MonedaCompra = transaccionDtoRequest.MonedaCompra,
                    MontoCambioDia = cotizacionMoneda.CambioVenta,
                    MontoCompra = montoCompra,
                    FechaTransaccion = DateTime.Now
                };
                await _context.Transaccion.AddAsync(nuevaTransaccion);
                result = await _context.SaveChangesAsync() > 0;
            }

            return result;
        }

        public async Task<IEnumerable<TransaccionDto>> GetTransacciones()
        {
            var transacciones = await _context.Transaccion.OrderByDescending(x => x.FechaTransaccion).ToListAsync();
            return transacciones;
        }
    }
}