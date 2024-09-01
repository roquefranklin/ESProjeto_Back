using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESProjeto_Back.Migrations
{
    /// <inheritdoc />
    public partial class PasswordRecovery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RecoveryCode",
                table: "Users",
                type: "varchar(6)",
                maxLength: 6,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecoveryCode",
                table: "Users");
        }
    }
}
