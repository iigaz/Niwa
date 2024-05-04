using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Niwa.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedFilename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "File",
                table: "NoteFiles",
                newName: "Filename");

            migrationBuilder.AddColumn<string>(
                name: "FileUrl",
                table: "NoteFiles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaf91a62-1964-46c6-ab36-a95af1486272"),
                column: "PasswordHash",
                value: "XTYHq+WZNY5kuWe+SkajjkXq+Mzau8eGyFA7/fZAs0M=;MHa3+lpwYT0S+mAzu/p2jg==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileUrl",
                table: "NoteFiles");

            migrationBuilder.RenameColumn(
                name: "Filename",
                table: "NoteFiles",
                newName: "File");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaf91a62-1964-46c6-ab36-a95af1486272"),
                column: "PasswordHash",
                value: "YhRy1p/jTuw61q33oAHP9BbDK5W8BSHWVcv5bM7GrCM=;eidZh1k0jsbUIEW3zapBiA==");
        }
    }
}
