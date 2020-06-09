using System.Collections.Generic;
using System.Threading.Tasks;
using VirtualMindServicesBackend.Dtos;

namespace VirtualMindServicesBackend.Interfaces
{
    public interface ITransaccionMoneda
    {
        Task<bool> GenerarTransaccion(TransaccionDtoRequest transaccionDtoRequest);

        Task<IEnumerable<TransaccionDto>> GetTransacciones();
    }
}