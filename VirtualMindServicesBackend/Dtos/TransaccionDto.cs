using System;
using System.ComponentModel.DataAnnotations;

namespace VirtualMindServicesBackend.Dtos
{
    public class TransaccionDto 
    {
        [Key]
        public Guid IdTransaccion { get; set; }
        [StringLength(20, MinimumLength = 4)]
        public string IdUsuario { get; set; }
        public decimal MontoPesosArgentinos { get; set; }
        [Required()]
        [StringLength(10, MinimumLength = 4)]
        public string MonedaCompra { get; set; }
        public decimal MontoCambioDia { get; set; }
        public decimal MontoCompra { get; set; }
        public DateTime FechaTransaccion { get; set; }
    }
}