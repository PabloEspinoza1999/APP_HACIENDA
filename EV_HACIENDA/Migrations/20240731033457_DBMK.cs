using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EV_HACIENDA.Migrations
{
    /// <inheritdoc />
    public partial class DBMK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LineasDetalles_FacturaElectronicaId",
                table: "LineasDetalles");

            migrationBuilder.DropColumn(
                name: "LineaDetalleId",
                table: "FacturasElectronicas");

            migrationBuilder.CreateIndex(
                name: "IX_LineasDetalles_FacturaElectronicaId",
                table: "LineasDetalles",
                column: "FacturaElectronicaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LineasDetalles_FacturaElectronicaId",
                table: "LineasDetalles");

            migrationBuilder.AddColumn<int>(
                name: "LineaDetalleId",
                table: "FacturasElectronicas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LineasDetalles_FacturaElectronicaId",
                table: "LineasDetalles",
                column: "FacturaElectronicaId",
                unique: true);
        }
    }
}
