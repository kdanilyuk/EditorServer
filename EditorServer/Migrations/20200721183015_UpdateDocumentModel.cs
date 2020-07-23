using Microsoft.EntityFrameworkCore.Migrations;

namespace EditorServer.Migrations
{
    public partial class UpdateDocumentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentOrder",
                table: "Documents",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentOrder",
                table: "Documents");
        }
    }
}
