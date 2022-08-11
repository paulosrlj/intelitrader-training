using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace user_api.Migrations
{
    public partial class changeUsersTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GetUsers",
                table: "GetUsers");

            migrationBuilder.RenameTable(
                name: "GetUsers",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "GetUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GetUsers",
                table: "GetUsers",
                column: "id");
        }
    }
}
