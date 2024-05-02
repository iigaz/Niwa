using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Niwa.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedFeaturedNotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GardenNote",
                columns: table => new
                {
                    FeaturedNotesId = table.Column<Guid>(type: "uuid", nullable: false),
                    Garden1Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GardenNote", x => new { x.FeaturedNotesId, x.Garden1Id });
                    table.ForeignKey(
                        name: "FK_GardenNote_Gardens_Garden1Id",
                        column: x => x.Garden1Id,
                        principalTable: "Gardens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GardenNote_Notes_FeaturedNotesId",
                        column: x => x.FeaturedNotesId,
                        principalTable: "Notes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaf91a62-1964-46c6-ab36-a95af1486272"),
                column: "PasswordHash",
                value: "EFrm/pSySZpLVXvTfgl0fET+PFLNU/PlhX3ozd8WyMo=;9eLBYGk7qYv1TuRJodGaYw==");

            migrationBuilder.CreateIndex(
                name: "IX_GardenNote_Garden1Id",
                table: "GardenNote",
                column: "Garden1Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GardenNote");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaf91a62-1964-46c6-ab36-a95af1486272"),
                column: "PasswordHash",
                value: "Ipe1jryJkKJ0OKMPKmQ2er0Ng8FLVQhL87uBw0pZSdE=;9gCDPkWvUnYA2dT+bAw61w==");
        }
    }
}
