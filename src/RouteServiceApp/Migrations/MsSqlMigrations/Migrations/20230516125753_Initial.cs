using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RouteServiceAPP.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Localities",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localities", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartureTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExtraInfo = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    SeatStartNumber = table.Column<int>(type: "int", nullable: false),
                    SeatEndNumber = table.Column<int>(type: "int", nullable: false),
                    FromName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ToName = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Routes_Localities_FromName",
                        column: x => x.FromName,
                        principalTable: "Localities",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Routes_Localities_ToName",
                        column: x => x.ToName,
                        principalTable: "Localities",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookedRoutes",
                columns: table => new
                {
                    BookedRouteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteId = table.Column<int>(type: "int", nullable: false),
                    FromName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ToName = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookedRoutes", x => x.BookedRouteId);
                    table.ForeignKey(
                        name: "FK_BookedRoutes_Localities_FromName",
                        column: x => x.FromName,
                        principalTable: "Localities",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookedRoutes_Localities_ToName",
                        column: x => x.ToName,
                        principalTable: "Localities",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookedRoutes_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RouteLocalities",
                columns: table => new
                {
                    RouteId = table.Column<int>(type: "int", nullable: false),
                    OrdinalNumber = table.Column<int>(type: "int", nullable: false),
                    LocalityName = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteLocalities", x => new { x.RouteId, x.OrdinalNumber });
                    table.ForeignKey(
                        name: "FK_RouteLocalities_Localities_LocalityName",
                        column: x => x.LocalityName,
                        principalTable: "Localities",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RouteLocalities_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    BookedRouteId = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => new { x.BookedRouteId, x.Number });
                    table.ForeignKey(
                        name: "FK_Seats_BookedRoutes_BookedRouteId",
                        column: x => x.BookedRouteId,
                        principalTable: "BookedRoutes",
                        principalColumn: "BookedRouteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookedRoutes_FromName",
                table: "BookedRoutes",
                column: "FromName");

            migrationBuilder.CreateIndex(
                name: "IX_BookedRoutes_RouteId",
                table: "BookedRoutes",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_BookedRoutes_ToName",
                table: "BookedRoutes",
                column: "ToName");

            migrationBuilder.CreateIndex(
                name: "IX_RouteLocalities_LocalityName",
                table: "RouteLocalities",
                column: "LocalityName");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_FromName",
                table: "Routes",
                column: "FromName");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_ToName",
                table: "Routes",
                column: "ToName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RouteLocalities");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "BookedRoutes");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropTable(
                name: "Localities");
        }
    }
}
