using System.Threading.Tasks;
using VirtualMindServicesBackend.Dtos;

namespace VirtualMindServicesBackend.Interfaces
{
    public interface ICotizacionMoneda
    {
        Task<CotizacionDtoResponse> GetCotizacion(string moneda);
    }
}