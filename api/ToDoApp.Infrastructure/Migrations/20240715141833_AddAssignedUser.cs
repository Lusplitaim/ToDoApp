using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAssignedUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignedUserId",
                table: "Todos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Todos_AssignedUserId",
                table: "Todos",
                column: "AssignedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_AspNetUsers_AssignedUserId",
                table: "Todos",
                column: "AssignedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todos_AspNetUsers_AssignedUserId",
                table: "Todos");

            migrationBuilder.DropIndex(
                name: "IX_Todos_AssignedUserId",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "AssignedUserId",
                table: "Todos");
        }
    }
}
