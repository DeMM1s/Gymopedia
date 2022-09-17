using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Gymopedia.Data.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Coaches_CoachId",
                table: "Clients");

            migrationBuilder.DropTable(
                name: "CoachIdsList");

            migrationBuilder.DropIndex(
                name: "IX_Clients_CoachId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "CoachId",
                table: "Clients");

            migrationBuilder.CreateTable(
                name: "ClientCoach",
                columns: table => new
                {
                    ClientsId = table.Column<int>(type: "integer", nullable: false),
                    CoachIdsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientCoach", x => new { x.ClientsId, x.CoachIdsId });
                    table.ForeignKey(
                        name: "FK_ClientCoach_Clients_ClientsId",
                        column: x => x.ClientsId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientCoach_Coaches_CoachIdsId",
                        column: x => x.CoachIdsId,
                        principalTable: "Coaches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientCoach_CoachIdsId",
                table: "ClientCoach",
                column: "CoachIdsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientCoach");

            migrationBuilder.AddColumn<int>(
                name: "CoachId",
                table: "Clients",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CoachIdsList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<int>(type: "integer", nullable: true),
                    ThisCoachId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoachIdsList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoachIdsList_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CoachId",
                table: "Clients",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_CoachIdsList_ClientId",
                table: "CoachIdsList",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Coaches_CoachId",
                table: "Clients",
                column: "CoachId",
                principalTable: "Coaches",
                principalColumn: "Id");
        }
    }
}
