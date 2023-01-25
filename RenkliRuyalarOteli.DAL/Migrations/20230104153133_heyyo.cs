using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RenkliRuyalarOteli.DAL.Migrations
{
    /// <inheritdoc />
    public partial class heyyo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Odalar_OdaNo",
                table: "Odalar",
                column: "OdaNo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Odalar_OdaNo",
                table: "Odalar");
        }
    }
}
