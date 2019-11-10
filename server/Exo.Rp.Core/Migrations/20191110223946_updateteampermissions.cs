using Microsoft.EntityFrameworkCore.Migrations;

namespace server.Migrations
{
    public partial class updateteampermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PermissionId",
                table: "TeamMemberPermissions");

            migrationBuilder.AddColumn<ulong>(
                name: "Permissions",
                table: "TeamMemberPermissions",
                nullable: false,
                defaultValue: 0ul);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Permissions",
                table: "TeamMemberPermissions");

            migrationBuilder.AddColumn<int>(
                name: "PermissionId",
                table: "TeamMemberPermissions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
