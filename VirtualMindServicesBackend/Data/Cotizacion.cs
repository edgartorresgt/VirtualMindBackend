using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VirtualMindServicesBackend.Dtos;
using VirtualMindServicesBackend.Interfaces;

namespace VirtualMindServicesBackend.Data
{
    public class Cotizacion: ICotizacionMoneda
    {
        private readonly IHttpClientFactory _clientFactory;

        public Cotizacion(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<CotizacionDtoResponse> GetCotizacion(string moneda)
        {
            CotizacionDtoResponse result = null;
            var request = new HttpRequestMessage(HttpMethod.Get,
                "https://www.bancoprovincia.com.ar/Principal/Dolar");

            var client = _clientFactory.CreateClient("virtualMind");
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    var jsonReader = new JsonTextReader(new StreamReader(stream));
                    var objDeserialize = new JsonSerializer().Deserialize<string[]>(jsonReader);
                    if (objDeserialize != null)
                    {
                        var objServiceResponse = new CotizacionDto()
                        {
                            CambioDolarCompra = Convert.ToDecimal(objDeserialize[0]),
                            CambioDolarVenta = Convert.ToDecimal(objDeserialize[1]),
                            FechaActualizacion = objDeserialize[2]
                        };

                        result = moneda.ToLower() switch
                        {
                            "dolar" => new CotizacionDtoResponse()
                            {
                                CambioCompra = objServiceResponse.CambioDolarCompra,
                                CambioVenta = objServiceResponse.CambioDolarVenta,
                                FechaActualizacion = objServiceResponse.FechaActualizacion
                            },
                            "real" => new CotizacionDtoResponse()
                            {
                                CambioCompra = Math.Round(objServiceResponse.CambioDolarCompra / 4, 2),
                                CambioVenta = Math.Round(objServiceResponse.CambioDolarVenta / 4, 2),
                                FechaActualizacion = objServiceResponse.FechaActualizacion
                            },
                            _ => null
                        };
                    }
                }
            }

            return await Task.FromResult(result);
        }
    }
}