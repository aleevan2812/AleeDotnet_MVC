using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AleeBook.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addOrderTotalInOrderHeaderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "OrderTotal",
                table: "OrderHeaders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderTotal",
                table: "OrderHeaders");
        }
    }
}
