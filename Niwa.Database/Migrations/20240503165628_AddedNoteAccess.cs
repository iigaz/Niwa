using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Niwa.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedNoteAccess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Access",
                table: "Notes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaf91a62-1964-46c6-ab36-a95af1486272"),
                column: "PasswordHash",
                value: "YhRy1p/jTuw61q33oAHP9BbDK5W8BSHWVcv5bM7GrCM=;eidZh1k0jsbUIEW3zapBiA==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Access",
                table: "Notes");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaf91a62-1964-46c6-ab36-a95af1486272"),
                column: "PasswordHash",
                value: "EFrm/pSySZpLVXvTfgl0fET+PFLNU/PlhX3ozd8WyMo=;9eLBYGk7qYv1TuRJodGaYw==");
        }
    }
}
