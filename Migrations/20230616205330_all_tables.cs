using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Motion.Migrations
{
    /// <inheritdoc />
    public partial class all_tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DLocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    lat = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    lng = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DLocations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Did = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SLatitude = table.Column<float>(type: "real", nullable: false),
                    SLongitude = table.Column<float>(type: "real", nullable: false),
                    ELatitude = table.Column<float>(type: "real", nullable: false),
                    ELongitude = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.RequestId);
                });

            migrationBuilder.CreateTable(
                name: "Rides",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Did = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SLatitude = table.Column<float>(type: "real", nullable: false),
                    SLongitude = table.Column<float>(type: "real", nullable: false),
                    ELatitude = table.Column<float>(type: "real", nullable: false),
                    ELongitude = table.Column<float>(type: "real", nullable: false),
                    Stime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ETime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Distance = table.Column<float>(type: "real", nullable: false),
                    Fare = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rides", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DLocations");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "Rides");
        }
    }
}
