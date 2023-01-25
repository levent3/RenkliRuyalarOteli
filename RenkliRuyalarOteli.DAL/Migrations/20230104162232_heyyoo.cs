using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RenkliRuyalarOteli.DAL.Migrations
{
    /// <inheritdoc />
    public partial class heyyoo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Odalar_OdaNo",
                table: "Odalar");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Odalar_OdaNo",
                table: "Odalar",
                column: "OdaNo",
                unique: true);
        }
    }
}
