using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gymopedia.Data.Migrations
{
    public partial class telegram4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Until",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "currentNumberOfClients",
                table: "Sessions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Until",
                table: "Sessions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "currentNumberOfClients",
                table: "Sessions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
