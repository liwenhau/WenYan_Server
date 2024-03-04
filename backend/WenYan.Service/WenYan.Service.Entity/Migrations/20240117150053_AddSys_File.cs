using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WenYan.Service.Entity.Migrations
{
    /// <inheritdoc />
    public partial class AddSys_File : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sys_File",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false, comment: "主键"),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false, comment: "文件名称"),
                    ExtendName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true, comment: "文件扩展名"),
                    Src = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true, comment: "外链地址"),
                    FilePath = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true, comment: "文件路径"),
                    SizeKb = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true, comment: "文件大小KB"),
                    IsDir = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false, comment: "是否目录"),
                    DirId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true, comment: "上级目录id"),
                    Type = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false, comment: "文件类型"),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false, comment: "是否删除"),
                    CreateUserId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true, comment: "创建人"),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false, comment: "创建时间"),
                    ModifyUserId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true, comment: "修改人"),
                    ModifyTime = table.Column<DateTime>(type: "TEXT", nullable: false, comment: "修改时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_File", x => x.Id);
                },
                comment: "系统文件");

            migrationBuilder.InsertData(
                table: "Sys_File",
                columns: new[] { "Id", "CreateTime", "CreateUserId", "DirId", "ExtendName", "FilePath", "IsDir", "ModifyTime", "ModifyUserId", "Name", "SizeKb", "Src", "Type" },
                values: new object[] { "1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1", null, null, "/", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1", "Files", null, null, "Dir" });

            migrationBuilder.InsertData(
                table: "Sys_File",
                columns: new[] { "Id", "CreateTime", "CreateUserId", "DirId", "ExtendName", "FilePath", "ModifyTime", "ModifyUserId", "Name", "SizeKb", "Src", "Type" },
                values: new object[] { "2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1", "1", ".jpg", "/Files/avatar01.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1", "头像1", "120", "https://avatars.githubusercontent.com/u/43628298?v=4", "Image" });

            migrationBuilder.UpdateData(
                table: "Sys_User",
                keyColumn: "Id",
                keyValue: "1",
                column: "Password",
                value: "9ce20d2bc2d67cbda4918e1d5d3100fa");

            migrationBuilder.CreateIndex(
                name: "IX_Sys_File_Deleted",
                table: "Sys_File",
                column: "Deleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sys_File");

            migrationBuilder.UpdateData(
                table: "Sys_User",
                keyColumn: "Id",
                keyValue: "1",
                column: "Password",
                value: "1a50207b5dc3aade372204b6169bd01a");
        }
    }
}
