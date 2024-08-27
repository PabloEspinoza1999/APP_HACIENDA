using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EV_HACIENDA.Migrations
{
    /// <inheritdoc />
    public partial class DBMO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LineaDetalleId",
                table: "FacturasElectronicas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LineaDetalleId",
                table: "FacturasElectronicas");
        }
    }
}
