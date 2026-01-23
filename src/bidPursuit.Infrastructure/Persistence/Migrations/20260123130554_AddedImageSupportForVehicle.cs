using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bidPursuit.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedImageSupportForVehicle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string[]>(
                name: "Images",
                table: "Vehicles",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Images",
                table: "Vehicles");
        }
    }
}
