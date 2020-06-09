using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VirtualMindServicesBackend.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transaccion",
                columns: table => new
                {
                    IdTransaccion = table.Column<Guid>(nullable: false),
                    IdUsuario = table.Column<string>(maxLength: 20, nullable: true),
                    MontoPesosArgentinos = table.Column<decimal>(nullable: false),
                    MonedaCompra = table.Column<string>(maxLength: 10, nullable: true),
                    MontoCambioDia = table.Column<decimal>(nullable: false),
                    MontoCompra = table.Column<decimal>(nullable: false),
                    FechaTransaccion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaccion", x => x.IdTransaccion);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaccion");
        }
    }
}
