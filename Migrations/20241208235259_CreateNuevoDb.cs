using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoxNovaSoftAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreateNuevoDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriaProductos",
                columns: table => new
                {
                    IdCProd = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCProd = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    EstadoCProd = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaProductos", x => x.IdCProd);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCliente = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ApellidoCliente = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CedulaCliente = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    GeneroCliente = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    DireccionCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.IdCliente);
                });

            migrationBuilder.CreateTable(
                name: "SubCategoriaProductos",
                columns: table => new
                {
                    IdSubCProd = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCProd = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    EstadoCProd = table.Column<bool>(type: "bit", nullable: false),
                    IdCProd = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategoriaProductos", x => x.IdSubCProd);
                    table.ForeignKey(
                        name: "FK_SubCategoriaProductos_CategoriaProductos_IdCProd",
                        column: x => x.IdCProd,
                        principalTable: "CategoriaProductos",
                        principalColumn: "IdCProd",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carritos",
                columns: table => new
                {
                    IdDetalle = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    PrecioUnitario = table.Column<float>(type: "real", nullable: false),
                    Subtotal = table.Column<float>(type: "real", nullable: false, computedColumnSql: "[Cantidad] * [PrecioUnitario]")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carritos", x => x.IdDetalle);
                    table.ForeignKey(
                        name: "FK_Carritos_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carritos_IdCliente",
                table: "Carritos",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_CedulaCliente",
                table: "Clientes",
                column: "CedulaCliente",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoriaProductos_IdCProd",
                table: "SubCategoriaProductos",
                column: "IdCProd");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carritos");

            migrationBuilder.DropTable(
                name: "SubCategoriaProductos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "CategoriaProductos");
        }
    }
}
