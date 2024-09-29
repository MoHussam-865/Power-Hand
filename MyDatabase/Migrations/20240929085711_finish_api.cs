using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Power_Hand.Migrations
{
    /// <inheritdoc />
    public partial class finish_api : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Client",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Client");
        }
    }
}
