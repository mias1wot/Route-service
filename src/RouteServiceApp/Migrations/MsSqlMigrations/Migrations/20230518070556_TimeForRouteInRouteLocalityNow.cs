using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RouteServiceAPP.Infrastructure.Migrations
{
    public partial class TimeForRouteInRouteLocalityNow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Localities_FromName",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Localities_ToName",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Routes_FromName",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Routes_ToName",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "ArrivalTime",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "DepartureTime",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "FromName",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "ToName",
                table: "Routes");

            migrationBuilder.AddColumn<DateTime>(
                name: "ArrivalTime",
                table: "RouteLocalities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DepartureTime",
                table: "RouteLocalities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArrivalTime",
                table: "RouteLocalities");

            migrationBuilder.DropColumn(
                name: "DepartureTime",
                table: "RouteLocalities");

            migrationBuilder.AddColumn<DateTime>(
                name: "ArrivalTime",
                table: "Routes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DepartureTime",
                table: "Routes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FromName",
                table: "Routes",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToName",
                table: "Routes",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Routes_FromName",
                table: "Routes",
                column: "FromName");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_ToName",
                table: "Routes",
                column: "ToName");

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Localities_FromName",
                table: "Routes",
                column: "FromName",
                principalTable: "Localities",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Localities_ToName",
                table: "Routes",
                column: "ToName",
                principalTable: "Localities",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
