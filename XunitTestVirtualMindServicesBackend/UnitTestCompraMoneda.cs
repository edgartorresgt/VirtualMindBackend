using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using VirtualMindServicesBackend.Controllers;
using VirtualMindServicesBackend.Dtos;
using VirtualMindServicesBackend.Interfaces;
using Xunit;

namespace XunitTestVirtualMindServicesBackend
{
    public class UnitTestCompraMoneda
    {
  
        [Fact]
        public async void CompraMonedaTest()
        {
            // Arrange
            var expected = StatusCodes.Status200OK;
            var loggerMock = new Mock<ILogger<CompraMonedaController>>();
            var mockRepository = new Mock<ITransaccionMoneda>();
            mockRepository.Setup(x => x.GenerarTransaccion(It.IsAny<TransaccionDtoRequest>()))
                .Returns(Task.FromResult(true));
            

            var controller = new CompraMonedaController(mockRepository.Object, loggerMock.Object);

            //act 
            var actionResult = await controller.Post(new TransaccionDtoRequest(){IdUsuario = "Edgar", MonedaCompra = "Dolar", MontoPesosArgentinos = 1000});
            var actual = ((StatusCodeResult)actionResult).StatusCode;

            //Assert 
            Assert.Equal(expected, actual);
        }
    }
}
