using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Niwa.Database.Migrations
{
    /// <inheritdoc />
    public partial class RemovedRolesAddedModerator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Label",
                value: "moderator");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaf91a62-1964-46c6-ab36-a95af1486272"),
                column: "PasswordHash",
                value: "x2N9nTEf0l7owBSaEyzeFFfB4i6PrFDkPmGveCPbOO4=;HHKoius1ad9Chwx1ZXmg4g==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Label",
                value: "viewer");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Label" },
                values: new object[,]
                {
                    { 2, "editor" },
                    { 3, "commentator" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaf91a62-1964-46c6-ab36-a95af1486272"),
                column: "PasswordHash",
                value: "BLbHwG7Eui2aV4IzvzZcR9IZc57kw9BUrJt9RPMhkYU=;w3ecK1DTNBsZbf5R1hQgDw==");
        }
    }
}
