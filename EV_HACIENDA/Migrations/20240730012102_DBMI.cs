using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EV_HACIENDA.Migrations
{
    /// <inheritdoc />
    public partial class DBMI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Codigo",
                columns: table => new
                {
                    CodigoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodigoValor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Codigo", x => x.CodigoId);
                });

            migrationBuilder.CreateTable(
                name: "Emisores",
                columns: table => new
                {
                    EmisorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Identificacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Provincia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Canton = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Distrito = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Barrio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OtrasSenas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodigoPais = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emisores", x => x.EmisorId);
                });

            migrationBuilder.CreateTable(
                name: "Impuestos",
                columns: table => new
                {
                    ImpuestoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tarifa = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Impuestos", x => x.ImpuestoId);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    ProductoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.ProductoId);
                });

            migrationBuilder.CreateTable(
                name: "Receptores",
                columns: table => new
                {
                    ReceptorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Identificacion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receptores", x => x.ReceptorId);
                });

            migrationBuilder.CreateTable(
                name: "ResumenFacturas",
                columns: table => new
                {
                    ResumenFacturaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalGravado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalImpuesto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalComprobante = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumenFacturas", x => x.ResumenFacturaId);
                });

            migrationBuilder.CreateTable(
                name: "FacturasElectronicas",
                columns: table => new
                {
                    FacturaElectronicaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroConsecutivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaEmision = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    EmisorId = table.Column<int>(type: "int", nullable: false),
                    ReceptorId = table.Column<int>(type: "int", nullable: false),
                    ResumenFacturaId = table.Column<int>(type: "int", nullable: false),
                    EstadoEnvio = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacturasElectronicas", x => x.FacturaElectronicaId);
                    table.ForeignKey(
                        name: "FK_FacturasElectronicas_Emisores_EmisorId",
                        column: x => x.EmisorId,
                        principalTable: "Emisores",
                        principalColumn: "EmisorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacturasElectronicas_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacturasElectronicas_Receptores_ReceptorId",
                        column: x => x.ReceptorId,
                        principalTable: "Receptores",
                        principalColumn: "ReceptorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacturasElectronicas_ResumenFacturas_ResumenFacturaId",
                        column: x => x.ResumenFacturaId,
                        principalTable: "ResumenFacturas",
                        principalColumn: "ResumenFacturaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LineasDetalles",
                columns: table => new
                {
                    LineaDetalleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroLinea = table.Column<int>(type: "int", nullable: false),
                    Detalle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MontoTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImpuestoId = table.Column<int>(type: "int", nullable: false),
                    FacturaElectronicaId = table.Column<int>(type: "int", nullable: false),
                    CodigoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnidadMedida = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MontoDescuento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NaturalezaDescuento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MontoTotalLinea = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineasDetalles", x => x.LineaDetalleId);
                    table.ForeignKey(
                        name: "FK_LineasDetalles_Codigo_CodigoId",
                        column: x => x.CodigoId,
                        principalTable: "Codigo",
                        principalColumn: "CodigoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineasDetalles_FacturasElectronicas_FacturaElectronicaId",
                        column: x => x.FacturaElectronicaId,
                        principalTable: "FacturasElectronicas",
                        principalColumn: "FacturaElectronicaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineasDetalles_Impuestos_ImpuestoId",
                        column: x => x.ImpuestoId,
                        principalTable: "Impuestos",
                        principalColumn: "ImpuestoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FacturasElectronicas_EmisorId",
                table: "FacturasElectronicas",
                column: "EmisorId");

            migrationBuilder.CreateIndex(
                name: "IX_FacturasElectronicas_ProductoId",
                table: "FacturasElectronicas",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_FacturasElectronicas_ReceptorId",
                table: "FacturasElectronicas",
                column: "ReceptorId");

            migrationBuilder.CreateIndex(
                name: "IX_FacturasElectronicas_ResumenFacturaId",
                table: "FacturasElectronicas",
                column: "ResumenFacturaId");

            migrationBuilder.CreateIndex(
                name: "IX_LineasDetalles_CodigoId",
                table: "LineasDetalles",
                column: "CodigoId");

            migrationBuilder.CreateIndex(
                name: "IX_LineasDetalles_FacturaElectronicaId",
                table: "LineasDetalles",
                column: "FacturaElectronicaId");

            migrationBuilder.CreateIndex(
                name: "IX_LineasDetalles_ImpuestoId",
                table: "LineasDetalles",
                column: "ImpuestoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LineasDetalles");

            migrationBuilder.DropTable(
                name: "Codigo");

            migrationBuilder.DropTable(
                name: "FacturasElectronicas");

            migrationBuilder.DropTable(
                name: "Impuestos");

            migrationBuilder.DropTable(
                name: "Emisores");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Receptores");

            migrationBuilder.DropTable(
                name: "ResumenFacturas");
        }
    }
}
