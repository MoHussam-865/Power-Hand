using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Power_Hand.Migrations
{
    /// <inheritdoc />
    public partial class uniq1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Emploee_Name",
                table: "Emploee",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Emploee_Password",
                table: "Emploee",
                column: "Password");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Emploee_Name",
                table: "Emploee");

            migrationBuilder.DropIndex(
                name: "IX_Emploee_Password",
                table: "Emploee");
        }
    }
}
