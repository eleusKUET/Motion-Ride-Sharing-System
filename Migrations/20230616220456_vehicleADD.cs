using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Motion.Migrations
{
    /// <inheritdoc />
    public partial class vehicleADD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Vehicle",
                table: "DLocations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Vehicle",
                table: "DLocations");
        }
    }
}
