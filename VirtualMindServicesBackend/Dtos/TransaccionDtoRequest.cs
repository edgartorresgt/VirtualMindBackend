namespace VirtualMindServicesBackend.Dtos
{
    public class TransaccionDtoRequest
    {
        public string IdUsuario { get; set; }
        public decimal MontoPesosArgentinos { get; set; }
        public string MonedaCompra { get; set; }
    }
}