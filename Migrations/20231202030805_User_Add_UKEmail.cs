using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Libertas.Migrations
{
    /// <inheritdoc />
    public partial class User_Add_UKEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "uk_email",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "uk_email",
                table: "users");
        }
    }
}
