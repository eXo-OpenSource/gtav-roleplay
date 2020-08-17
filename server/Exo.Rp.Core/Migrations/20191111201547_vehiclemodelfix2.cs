using Microsoft.EntityFrameworkCore.Migrations;

namespace server.Migrations
{
	public partial class vehiclemodelfix2 : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<uint>(
				name: "Model",
				table: "Vehicles",
				nullable: false,
				oldClrType: typeof(long),
				oldType: "bigint");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<long>(
				name: "Model",
				table: "Vehicles",
				type: "bigint",
				nullable: false,
				oldClrType: typeof(uint));
		}
	}
}
