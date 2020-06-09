using System.Linq;
using Microsoft.AspNetCore.Http;

namespace VirtualMindServicesBackend.Helper
{
    public static class Extensiones
    {
        private static readonly string[] StringArray = { "dolar", "real" };

        public static string MonedaValida(string monedaCompra)
        {
            if (!StringArray.Any(x => x.Equals(monedaCompra.ToLower())))
                return "Disculpe los inconvenientes por el momento solo aceptamos compras de divisas para dolares o reales";
            return "";
        }
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }
    }
}