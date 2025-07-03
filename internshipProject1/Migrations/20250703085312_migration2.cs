using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace internshipProject1.Migrations
{
    /// <inheritdoc />
    public partial class migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RouteStop_Route_StopId",
                table: "RouteStop");

            migrationBuilder.CreateIndex(
                name: "IX_RouteStop_RouteId",
                table: "RouteStop",
                column: "RouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_RouteStop_Route_RouteId",
                table: "RouteStop",
                column: "RouteId",
                principalTable: "Route",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RouteStop_Route_RouteId",
                table: "RouteStop");

            migrationBuilder.DropIndex(
                name: "IX_RouteStop_RouteId",
                table: "RouteStop");

            migrationBuilder.AddForeignKey(
                name: "FK_RouteStop_Route_StopId",
                table: "RouteStop",
                column: "StopId",
                principalTable: "Route",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
