using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Niwa.Database.Migrations
{
    /// <inheritdoc />
    public partial class EmptyMigrationToFixPasswordConsistency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaf91a62-1964-46c6-ab36-a95af1486272"),
                column: "PasswordHash",
                value: "SuAXex8b0XkT/edfOK4u1qQfqSNVKlBeLqfk1ZAHOsg=;gsBfnorqA0irnJd155F5Mw==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaf91a62-1964-46c6-ab36-a95af1486272"),
                column: "PasswordHash",
                value: "TOtQd+OOYtLwvvwdwph3ovZn9Bz2sCsQ26CG/keH/0g=;po//Xdg4nknQnWe/8XDM3A==");
        }
    }
}
