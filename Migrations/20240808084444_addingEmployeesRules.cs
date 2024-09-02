using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Power_Hand.Migrations
{
    /// <inheritdoc />
    public partial class addingEmployeesRules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Emploee",
                table: "Emploee");

            migrationBuilder.RenameTable(
                name: "Emploee",
                newName: "Employee");

            migrationBuilder.RenameIndex(
                name: "IX_Emploee_Password",
                table: "Employee",
                newName: "IX_Employee_Password");

            migrationBuilder.RenameIndex(
                name: "IX_Emploee_Name",
                table: "Employee",
                newName: "IX_Employee_Name");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Employee",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Employee",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "Rule",
                table: "Employee",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employee",
                table: "Employee",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Settings_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Settings_EmployeeId",
                table: "Settings",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employee",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "Rule",
                table: "Employee");

            migrationBuilder.RenameTable(
                name: "Employee",
                newName: "Emploee");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_Password",
                table: "Emploee",
                newName: "IX_Emploee_Password");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_Name",
                table: "Emploee",
                newName: "IX_Emploee_Name");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Emploee",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Emploee",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Emploee",
                table: "Emploee",
                column: "Id");
        }
    }
}
