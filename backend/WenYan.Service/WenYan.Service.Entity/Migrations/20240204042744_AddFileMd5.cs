using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WenYan.Service.Entity.Migrations
{
    /// <inheritdoc />
    public partial class AddFileMd5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sys_File",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.AddColumn<string>(
                name: "FileMd5",
                table: "Sys_File",
                type: "TEXT",
                maxLength: 100,
                nullable: true,
                comment: "文件MD5");

            migrationBuilder.UpdateData(
                table: "Sys_File",
                keyColumn: "Id",
                keyValue: "1",
                column: "FileMd5",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileMd5",
                table: "Sys_File");

            migrationBuilder.InsertData(
                table: "Sys_File",
                columns: new[] { "Id", "CreateTime", "CreateUserId", "DirId", "ExtendName", "FilePath", "ModifyTime", "ModifyUserId", "Name", "SizeKb", "Src", "Type" },
                values: new object[] { "2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1", "1", ".jpg", "/Files/avatar01.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1", "头像1", "120", "https://avatars.githubusercontent.com/u/43628298?v=4", "Image" });
        }
    }
}
