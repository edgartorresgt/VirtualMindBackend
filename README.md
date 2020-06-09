# VirtualMindBackend

Los scripts para la creacion de la base de datos se encuentran dentro de la carpeta *VirtualMindServicesBackend*.

Esta solucion fue creada con .net core 3.1, utilizando entity framework. Estos scripts son para SQL Server, Si no desean utilizar los scripts, desde la ubicacion donde clonen la solucion (Por favor verificar que tengan instalado .net core 3.1 framework). Por favor ejecutar los siguientes comandos en el siguiente orden: 

1. dotnet ef migrations remove
2. dotnet build
3. dotnet ef migrations add InitialCreate 
4. dotnet ef database update

Con esto se generaran automaticamente los objetos en una base de datos SQL. 
