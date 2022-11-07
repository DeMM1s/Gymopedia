using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Gymopedia.Data.Migrations
{
    public partial class telegramChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Coaches_CoachId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Coaches_Calendar_CalendarId",
                table: "Coaches");

            migrationBuilder.DropForeignKey(
                name: "FK_CoachWorkDays_Calendar_CalendarId",
                table: "CoachWorkDays");

            migrationBuilder.DropTable(
                name: "Calendar");

            migrationBuilder.DropTable(
                name: "ClientSession");

            migrationBuilder.DropTable(
                name: "Ids");

            migrationBuilder.DropIndex(
                name: "IX_CoachWorkDays_CalendarId",
                table: "CoachWorkDays");

            migrationBuilder.DropIndex(
                name: "IX_Coaches_CalendarId",
                table: "Coaches");

            migrationBuilder.DropIndex(
                name: "IX_Clients_CoachId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "CalendarId",
                table: "CoachWorkDays");

            migrationBuilder.DropColumn(
                name: "CalendarId",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "CoachId",
                table: "Clients");

            migrationBuilder.AddColumn<long>(
                name: "ChatId",
                table: "Coaches",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ChatId",
                table: "Clients",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "ClientToCoach",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CoachId = table.Column<int>(type: "integer", nullable: false),
                    ClientId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientToCoach", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientToSession",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SessionId = table.Column<int>(type: "integer", nullable: false),
                    ClientId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientToSession", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientToCoach");

            migrationBuilder.DropTable(
                name: "ClientToSession");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "Clients");

            migrationBuilder.AddColumn<int>(
                name: "CalendarId",
                table: "CoachWorkDays",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CalendarId",
                table: "Coaches",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CoachId",
                table: "Clients",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Calendar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OwnerCoachId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendar", x => x.Id);
                });

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
                    ClientId = table.Column<int>(type: "integer", nullable: true),
                    externalId = table.Column<int>(type: "integer", nullable: false)
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
                name: "IX_CoachWorkDays_CalendarId",
                table: "CoachWorkDays",
                column: "CalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_Coaches_CalendarId",
                table: "Coaches",
                column: "CalendarId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Coaches_Calendar_CalendarId",
                table: "Coaches",
                column: "CalendarId",
                principalTable: "Calendar",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CoachWorkDays_Calendar_CalendarId",
                table: "CoachWorkDays",
                column: "CalendarId",
                principalTable: "Calendar",
                principalColumn: "Id");
        }
    }
}
