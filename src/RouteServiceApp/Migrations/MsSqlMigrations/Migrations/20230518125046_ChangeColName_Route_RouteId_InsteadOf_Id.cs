using Microsoft.EntityFrameworkCore.Migrations;

namespace RouteServiceAPP.Infrastructure.Migrations
{
    public partial class ChangeColName_Route_RouteId_InsteadOf_Id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Routes",
                newName: "RouteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RouteId",
                table: "Routes",
                newName: "Id");
        }
    }
}
