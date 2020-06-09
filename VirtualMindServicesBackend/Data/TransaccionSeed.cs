using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using VirtualMindServicesBackend.Dtos;

namespace VirtualMindServicesBackend.Data
{
    public class TransaccionSeed
    {
        public static void SeedTransacciones(DataContext context)
        {
            if (!context.Transaccion.Any())
            {
                var transaccionData = System.IO.File.ReadAllText("Data/TransacionDataSeed.json");
                var transacciones = JsonConvert.DeserializeObject<List<TransaccionDto>>(transaccionData);
                foreach (var transaccion in transacciones)
                {
                    transaccion.IdTransaccion = Guid.NewGuid();
                    transaccion.FechaTransaccion = DateTime.Now;
                    context.Transaccion.Add(transaccion);
                }

                context.SaveChanges();
            }
        }
    }
}