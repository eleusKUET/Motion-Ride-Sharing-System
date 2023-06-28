using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Motion.Migrations
{
    /// <inheritdoc />
    public partial class df : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "lng",
                table: "DLocations",
                type: "float",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<double>(
                name: "lat",
                table: "DLocations",
                type: "float",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "lng",
                table: "DLocations",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<Guid>(
                name: "lat",
                table: "DLocations",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
