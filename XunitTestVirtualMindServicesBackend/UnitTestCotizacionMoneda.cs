using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using VirtualMindServicesBackend.Controllers;
using VirtualMindServicesBackend.Dtos;
using VirtualMindServicesBackend.Interfaces;
using Xunit;

namespace XunitTestVirtualMindServicesBackend
{
    public class UnitTestCotizacionMoneda
    {
  
        [Fact]
        public async void CotizacionMonedaTest()
        {
            // Arrange
            var expected = new CotizacionDtoResponse()
                {CambioVenta = 1, CambioCompra = 2, FechaActualizacion = "08/06/2020"};
            var loggerMock = new Mock<ILogger<CotizacionMonedaController>>();
            var mockRepository = new Mock<ICotizacionMoneda>();
            mockRepository.Setup(x => x.GetCotizacion(It.IsAny<string>()))
                .Returns(Task.FromResult(new CotizacionDtoResponse(){CambioVenta = 1, CambioCompra = 2, FechaActualizacion = "08/06/2020"}));

            var controller = new  CotizacionMonedaController(mockRepository.Object, loggerMock.Object);

            //act 
            var actionResult = await controller.GetCotizacion("Dolar");
            var actual = (CotizacionDtoResponse)((ObjectResult)actionResult).Value;

            //Assert 
            Assert.Equal(expected.CambioCompra, actual.CambioCompra);
            Assert.Equal(expected.CambioVenta, actual.CambioVenta);
            Assert.Equal(expected.FechaActualizacion, actual.FechaActualizacion);
        }
    }
}
