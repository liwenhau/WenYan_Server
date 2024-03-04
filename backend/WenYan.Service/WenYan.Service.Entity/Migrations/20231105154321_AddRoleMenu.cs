using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WenYan.Service.Entity.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleMenu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "Sys_UserRole");

            migrationBuilder.DropColumn(
                name: "CreateUserId",
                table: "Sys_UserRole");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Sys_UserRole");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Sys_UserRole");

            migrationBuilder.DropColumn(
                name: "ModifyTime",
                table: "Sys_UserRole");

            migrationBuilder.DropColumn(
                name: "ModifyUserId",
                table: "Sys_UserRole");

            migrationBuilder.CreateTable(
                name: "Sys_RoleMenu",
                columns: table => new
                {
                    RoleId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    MenuId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_RoleMenu", x => new { x.MenuId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_Sys_RoleMenu_Sys_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Sys_Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sys_RoleMenu_Sys_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Sys_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "角色菜单");

            migrationBuilder.InsertData(
                table: "Sys_RoleMenu",
                columns: new[] { "MenuId", "RoleId" },
                values: new object[,]
                {
                    { "100", "1" },
                    { "101", "1" },
                    { "900", "1" },
                    { "901", "1" },
                    { "9010", "1" },
                    { "9011", "1" },
                    { "9012", "1" },
                    { "9013", "1" },
                    { "902", "1" },
                    { "903", "1" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sys_RoleMenu_RoleId",
                table: "Sys_RoleMenu",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sys_RoleMenu");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "Sys_UserRole",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreateUserId",
                table: "Sys_UserRole",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Sys_UserRole",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Sys_UserRole",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyTime",
                table: "Sys_UserRole",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifyUserId",
                table: "Sys_UserRole",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Sys_UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "1" },
                columns: new[] { "CreateTime", "CreateUserId", "Deleted", "Id", "ModifyTime", "ModifyUserId" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });
        }
    }
}
