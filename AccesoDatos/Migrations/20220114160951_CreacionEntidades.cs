using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AplicacionWeb.Data.Migrations
{
    public partial class CreacionEntidades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombres = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Direccion = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    Telefono = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    CreadoEn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoEn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NombreUsuario = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cuentas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Saldo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreadoEn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoEn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdCliente = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuentas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cuentas_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Movimientos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EsCredito = table.Column<bool>(type: "bit", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ActualizadoEn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdCuenta = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movimientos_Cuentas_IdCuenta",
                        column: x => x.IdCuenta,
                        principalTable: "Cuentas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cuentas_IdCliente",
                table: "Cuentas",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_IdCuenta",
                table: "Movimientos",
                column: "IdCuenta");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movimientos");

            migrationBuilder.DropTable(
                name: "Cuentas");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
