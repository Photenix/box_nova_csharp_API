using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoxNovaSoftAPI.Migrations
{
    /// <inheritdoc />
    public partial class diciembre_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubCategoriaProductos_CategoriaProducto_IdCProd",
                table: "SubCategoriaProductos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoriaProducto",
                table: "CategoriaProducto");

            migrationBuilder.RenameTable(
                name: "CategoriaProducto",
                newName: "CategoriaProductos");

            migrationBuilder.AddColumn<string>(
                name: "Categoria",
                table: "Productos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "categoriaIdCProd",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoriaProductos",
                table: "CategoriaProductos",
                column: "IdCProd");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_categoriaIdCProd",
                table: "Productos",
                column: "categoriaIdCProd");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_CategoriaProductos_categoriaIdCProd",
                table: "Productos",
                column: "categoriaIdCProd",
                principalTable: "CategoriaProductos",
                principalColumn: "IdCProd",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategoriaProductos_CategoriaProductos_IdCProd",
                table: "SubCategoriaProductos",
                column: "IdCProd",
                principalTable: "CategoriaProductos",
                principalColumn: "IdCProd",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_CategoriaProductos_categoriaIdCProd",
                table: "Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategoriaProductos_CategoriaProductos_IdCProd",
                table: "SubCategoriaProductos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_categoriaIdCProd",
                table: "Productos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoriaProductos",
                table: "CategoriaProductos");

            migrationBuilder.DropColumn(
                name: "Categoria",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "categoriaIdCProd",
                table: "Productos");

            migrationBuilder.RenameTable(
                name: "CategoriaProductos",
                newName: "CategoriaProducto");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoriaProducto",
                table: "CategoriaProducto",
                column: "IdCProd");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategoriaProductos_CategoriaProducto_IdCProd",
                table: "SubCategoriaProductos",
                column: "IdCProd",
                principalTable: "CategoriaProducto",
                principalColumn: "IdCProd",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
