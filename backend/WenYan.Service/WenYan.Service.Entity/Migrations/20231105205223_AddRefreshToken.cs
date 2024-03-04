using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WenYan.Service.Entity.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HeadPortrait",
                table: "Sys_User",
                newName: "Avatar");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Sys_User",
                type: "TEXT",
                maxLength: 256,
                nullable: true,
                comment: "刷新Token");

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "Sys_User",
                type: "TEXT",
                nullable: true,
                comment: "刷新Token过期时间");

            migrationBuilder.UpdateData(
                table: "Sys_User",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "RefreshToken", "RefreshTokenExpiryTime" },
                values: new object[] { null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Sys_User");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "Sys_User");

            migrationBuilder.RenameColumn(
                name: "Avatar",
                table: "Sys_User",
                newName: "HeadPortrait");
        }
    }
}
