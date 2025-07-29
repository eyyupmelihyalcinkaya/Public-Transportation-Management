using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace internshipProject1.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "CardTransaction",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TransactionType",
                table: "CardTransaction",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                table: "CardTransaction");

            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "CardTransaction");
        }
    }
}
