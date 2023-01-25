using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RenkliRuyalarOteli.DAL.Migrations
{
    /// <inheritdoc />
    public partial class odayaimagealanıeklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Odalar",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Odalar");
        }
    }
}
