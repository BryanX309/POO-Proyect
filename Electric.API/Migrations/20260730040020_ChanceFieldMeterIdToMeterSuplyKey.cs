using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Electric.API.Migrations
{
    /// <inheritdoc />
    public partial class ChanceFieldMeterIdToMeterSuplyKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bills_meters_meter_id",
                table: "bills");

            migrationBuilder.RenameColumn(
                name: "meter_id",
                table: "bills",
                newName: "meter_supply_Key");

            migrationBuilder.RenameIndex(
                name: "IX_bills_meter_id",
                table: "bills",
                newName: "IX_bills_meter_supply_Key");

            migrationBuilder.AddForeignKey(
                name: "FK_bills_meters_meter_supply_Key",
                table: "bills",
                column: "meter_supply_Key",
                principalTable: "meters",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bills_meters_meter_supply_Key",
                table: "bills");

            migrationBuilder.RenameColumn(
                name: "meter_supply_Key",
                table: "bills",
                newName: "meter_id");

            migrationBuilder.RenameIndex(
                name: "IX_bills_meter_supply_Key",
                table: "bills",
                newName: "IX_bills_meter_id");

            migrationBuilder.AddForeignKey(
                name: "FK_bills_meters_meter_id",
                table: "bills",
                column: "meter_id",
                principalTable: "meters",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
