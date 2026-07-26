using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Electric.API.Migrations
{
    /// <inheritdoc />
    public partial class TablesMetersAndBills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "meters",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    supply_key = table.Column<int>(type: "INTEGER", nullable: false),
                    client_Id = table.Column<string>(type: "TEXT", nullable: false),
                    consumption_type = table.Column<string>(type: "TEXT", nullable: true),
                    rate = table.Column<string>(type: "TEXT", nullable: true),
                    comercial_sector = table.Column<string>(type: "TEXT", nullable: true),
                    is_active = table.Column<bool>(type: "INTEGER", nullable: false),
                    created_by_id = table.Column<string>(type: "TEXT", nullable: true),
                    created_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    modified_by_id = table.Column<string>(type: "TEXT", nullable: true),
                    modified_date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meters", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "bills",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    meter_id = table.Column<string>(type: "TEXT", nullable: false),
                    expiration_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    total_amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    previous_reading = table.Column<int>(type: "INTEGER", nullable: false),
                    current_reading = table.Column<int>(type: "INTEGER", nullable: false),
                    previous_reading_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    current_reading_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    paid = table.Column<bool>(type: "INTEGER", nullable: false),
                    created_by_id = table.Column<string>(type: "TEXT", nullable: true),
                    created_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    modified_by_id = table.Column<string>(type: "TEXT", nullable: true),
                    modified_date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bills", x => x.id);
                    table.ForeignKey(
                        name: "FK_bills_meters_meter_id",
                        column: x => x.meter_id,
                        principalTable: "meters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bills_meter_id",
                table: "bills",
                column: "meter_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bills");

            migrationBuilder.DropTable(
                name: "meters");
        }
    }
}
