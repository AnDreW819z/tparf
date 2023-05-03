using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tparf.Migrations
{
    /// <inheritdoc />
    public partial class AddTotalOrdersPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalOrdersPrice",
                table: "Users",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalOrdersPrice",
                table: "Users");
        }
    }
}
