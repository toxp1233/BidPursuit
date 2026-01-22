using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bidPursuit.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedCarState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Vehicles",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Vehicles");
        }
    }
}
