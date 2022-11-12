using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gymopedia.Data.Migrations
{
    public partial class telegram3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CoachId",
                table: "Sessions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "currentNumberOfClients",
                table: "Sessions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoachId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "currentNumberOfClients",
                table: "Sessions");
        }
    }
}
