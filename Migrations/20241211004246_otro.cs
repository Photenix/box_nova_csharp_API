using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoxNovaSoftAPI.Migrations
{
    /// <inheritdoc />
    public partial class otro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NombreCProd",
                table: "SubCategoriaProductos",
                newName: "NombreSubCProd");

            migrationBuilder.RenameColumn(
                name: "EstadoCProd",
                table: "SubCategoriaProductos",
                newName: "EstadoSubCProd");

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

            migrationBuilder.CreateIndex(
                name: "IX_Productos_categoriaIdCProd",
                table: "Productos",
                column: "categoriaIdCProd");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_CategoriaProducto_categoriaIdCProd",
                table: "Productos",
                column: "categoriaIdCProd",
                principalTable: "CategoriaProducto",
                principalColumn: "IdCProd",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_CategoriaProducto_categoriaIdCProd",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_categoriaIdCProd",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Categoria",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "categoriaIdCProd",
                table: "Productos");

            migrationBuilder.RenameColumn(
                name: "NombreSubCProd",
                table: "SubCategoriaProductos",
                newName: "NombreCProd");

            migrationBuilder.RenameColumn(
                name: "EstadoSubCProd",
                table: "SubCategoriaProductos",
                newName: "EstadoCProd");
        }
    }
}
