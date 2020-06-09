using Microsoft.EntityFrameworkCore;
using VirtualMindServicesBackend.Dtos;

namespace VirtualMindServicesBackend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<TransaccionDto> Transaccion { get; set; }
    }
}