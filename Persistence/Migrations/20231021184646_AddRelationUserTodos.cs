using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationUserTodos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Todos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Todos_AppUserId",
                table: "Todos",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_AspNetUsers_AppUserId",
                table: "Todos",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todos_AspNetUsers_AppUserId",
                table: "Todos");

            migrationBuilder.DropIndex(
                name: "IX_Todos_AppUserId",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Todos");
        }
    }
}
