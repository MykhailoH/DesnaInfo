using Microsoft.EntityFrameworkCore.Migrations;

namespace DesnaInfo.DataAccess.Migrations
{
    public partial class UniqueIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_MessengerId",
                table: "Users",
                column: "MessengerId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_MessengerId",
                table: "Users");
        }
    }
}
