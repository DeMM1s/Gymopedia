using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Gymopedia.Data.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Clients_ClientId",
                table: "Sessions");

            migrationBuilder.DropTable(
                name: "ClientCoach");

            migrationBuilder.DropTable(
                name: "ClientIdsList");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_ClientId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Sessions");

            migrationBuilder.AddColumn<int>(
                name: "CoachId",
                table: "Clients",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClientSession",
                columns: table => new
                {
                    ClientsId = table.Column<int>(type: "integer", nullable: false),
                    TrainingSessionsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientSession", x => new { x.ClientsId, x.TrainingSessionsId });
                    table.ForeignKey(
                        name: "FK_ClientSession_Clients_ClientsId",
                        column: x => x.ClientsId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientSession_Sessions_TrainingSessionsId",
                        column: x => x.TrainingSessionsId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ids",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    externalId = table.Column<int>(type: "integer", nullable: false),
                    ClientId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ids_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CoachId",
                table: "Clients",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientSession_TrainingSessionsId",
                table: "ClientSession",
                column: "TrainingSessionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Ids_ClientId",
                table: "Ids",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Coaches_CoachId",
                table: "Clients",
                column: "CoachId",
                principalTable: "Coaches",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Coaches_CoachId",
                table: "Clients");

            migrationBuilder.DropTable(
                name: "ClientSession");

            migrationBuilder.DropTable(
                name: "Ids");

            migrationBuilder.DropIndex(
                name: "IX_Clients_CoachId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "CoachId",
                table: "Clients");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Sessions",
                type: "integer",
                nullable: true);

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

            migrationBuilder.CreateTable(
                name: "ClientIdsList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientOfThisCoachId = table.Column<int>(type: "integer", nullable: false),
                    SessionId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientIdsList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientIdsList_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_ClientId",
                table: "Sessions",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientCoach_CoachIdsId",
                table: "ClientCoach",
                column: "CoachIdsId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientIdsList_SessionId",
                table: "ClientIdsList",
                column: "SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Clients_ClientId",
                table: "Sessions",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");
        }
    }
}
