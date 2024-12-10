using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoxNovaSoftAPI.Migrations
{
    /// <inheritdoc />
    public partial class diciembre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriaProducto",
                columns: table => new
                {
                    IdCProd = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCProd = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    EstadoCProd = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaProducto", x => x.IdCProd);
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
                name: "Permisos",
                columns: table => new
                {
                    id_permiso = table.Column<int>(type: "int", nullable: false),
                    nombre_permiso = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    estado_permiso = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_permiso", x => x.id_permiso);
                });

            migrationBuilder.CreateTable(
                name: "Privilegios",
                columns: table => new
                {
                    id_privilegio = table.Column<int>(type: "int", nullable: false),
                    nombre_privilegio = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_privilegio", x => x.id_privilegio);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    IdProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreProducto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrecioProducto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockProducto = table.Column<int>(type: "int", nullable: false),
                    CategoriaProducto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClasificacionProducto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoProducto = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.IdProducto);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    id_rol = table.Column<int>(type: "int", nullable: false),
                    nombre_rol = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    estado_rol = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rol", x => x.id_rol);
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
                        name: "FK_SubCategoriaProductos_CategoriaProducto_IdCProd",
                        column: x => x.IdCProd,
                        principalTable: "CategoriaProducto",
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

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    IdPedido = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    MontoTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaDelPedido = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoPedido = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.IdPedido);
                    table.ForeignKey(
                        name: "FK_Pedidos_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    VentaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventas", x => x.VentaId);
                    table.ForeignKey(
                        name: "FK_Ventas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PerXRolXPriv",
                columns: table => new
                {
                    id_perxrol = table.Column<int>(type: "int", nullable: false),
                    id_per = table.Column<int>(type: "int", nullable: true),
                    id_rol = table.Column<int>(type: "int", nullable: true),
                    id_priv = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_perxrol", x => x.id_perxrol);
                    table.ForeignKey(
                        name: "fk_permiso",
                        column: x => x.id_per,
                        principalTable: "Permisos",
                        principalColumn: "id_permiso");
                    table.ForeignKey(
                        name: "fk_priv",
                        column: x => x.id_priv,
                        principalTable: "Privilegios",
                        principalColumn: "id_privilegio");
                    table.ForeignKey(
                        name: "fk_rol",
                        column: x => x.id_rol,
                        principalTable: "Roles",
                        principalColumn: "id_rol");
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "int", nullable: false),
                    tarjeta_identidad = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    correo_usuario = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    nombre_usuario = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    contrasena_usuario = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    cumpleano_usuario = table.Column<DateOnly>(type: "date", nullable: false),
                    fecha_creacion_usuario = table.Column<DateOnly>(type: "date", nullable: false),
                    genero_usuario = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    estado_usuario = table.Column<bool>(type: "bit", unicode: false, nullable: false, defaultValue: true),
                    fk_rol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id_usuario);
                    table.ForeignKey(
                        name: "fk_rol_user",
                        column: x => x.fk_rol,
                        principalTable: "Roles",
                        principalColumn: "id_rol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetallePedidos",
                columns: table => new
                {
                    IdDetPedido = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPedido = table.Column<int>(type: "int", nullable: false),
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallePedidos", x => x.IdDetPedido);
                    table.ForeignKey(
                        name: "FK_DetallePedidos_Pedidos_IdPedido",
                        column: x => x.IdPedido,
                        principalTable: "Pedidos",
                        principalColumn: "IdPedido",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallePedidos_Productos_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "Productos",
                        principalColumn: "IdProducto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleVentas",
                columns: table => new
                {
                    DetalleVentaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VentaId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleVentas", x => x.DetalleVentaId);
                    table.ForeignKey(
                        name: "FK_DetalleVentas_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "IdProducto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleVentas_Ventas_VentaId",
                        column: x => x.VentaId,
                        principalTable: "Ventas",
                        principalColumn: "VentaId",
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
                name: "IX_DetallePedidos_IdPedido",
                table: "DetallePedidos",
                column: "IdPedido");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedidos_IdProducto",
                table: "DetallePedidos",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentas_ProductoId",
                table: "DetalleVentas",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentas_VentaId",
                table: "DetalleVentas",
                column: "VentaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_IdCliente",
                table: "Pedidos",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "un_nombre_permiso",
                table: "Permisos",
                column: "nombre_permiso",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PerXRolXPriv_id_priv",
                table: "PerXRolXPriv",
                column: "id_priv");

            migrationBuilder.CreateIndex(
                name: "IX_PerXRolXPriv_id_rol",
                table: "PerXRolXPriv",
                column: "id_rol");

            migrationBuilder.CreateIndex(
                name: "un_perxrolxpriv",
                table: "PerXRolXPriv",
                columns: new[] { "id_per", "id_rol", "id_priv" },
                unique: true,
                filter: "[id_per] IS NOT NULL AND [id_rol] IS NOT NULL AND [id_priv] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "un_nombre_rol",
                table: "Roles",
                column: "nombre_rol",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoriaProductos_IdCProd",
                table: "SubCategoriaProductos",
                column: "IdCProd");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_fk_rol",
                table: "Usuarios",
                column: "fk_rol");

            migrationBuilder.CreateIndex(
                name: "un_correo_usuario",
                table: "Usuarios",
                column: "correo_usuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "un_tarjeta_identidad",
                table: "Usuarios",
                column: "tarjeta_identidad",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Usuarios__CC7F75E4F10CEE4D",
                table: "Usuarios",
                column: "tarjeta_identidad",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Usuarios__CD54AB1CAFE69B7A",
                table: "Usuarios",
                column: "correo_usuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_ClienteId",
                table: "Ventas",
                column: "ClienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carritos");

            migrationBuilder.DropTable(
                name: "DetallePedidos");

            migrationBuilder.DropTable(
                name: "DetalleVentas");

            migrationBuilder.DropTable(
                name: "PerXRolXPriv");

            migrationBuilder.DropTable(
                name: "SubCategoriaProductos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Ventas");

            migrationBuilder.DropTable(
                name: "Permisos");

            migrationBuilder.DropTable(
                name: "Privilegios");

            migrationBuilder.DropTable(
                name: "CategoriaProducto");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
