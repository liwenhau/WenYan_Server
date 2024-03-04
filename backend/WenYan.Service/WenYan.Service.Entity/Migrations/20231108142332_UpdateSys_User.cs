using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WenYan.Service.Entity.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSys_User : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Sign",
                table: "Sys_User",
                type: "TEXT",
                maxLength: 100,
                nullable: true,
                comment: "个人签名",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 100,
                oldComment: "个人签名");

            migrationBuilder.AlterColumn<string>(
                name: "Avatar",
                table: "Sys_User",
                type: "TEXT",
                maxLength: 2000,
                nullable: true,
                comment: "头像",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 2000,
                oldComment: "头像");

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "Sys_User",
                type: "TEXT",
                maxLength: 256,
                nullable: true,
                comment: "描述");

            migrationBuilder.UpdateData(
                table: "Sys_Menu",
                keyColumn: "Id",
                keyValue: "100",
                columns: new[] { "Icon", "Status" },
                values: new object[] { "menu-chart", "Enable" });

            migrationBuilder.UpdateData(
                table: "Sys_Menu",
                keyColumn: "Id",
                keyValue: "900",
                column: "Status",
                value: "Enable");

            migrationBuilder.UpdateData(
                table: "Sys_User",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "Remark", "Sign" },
                values: new object[] { "系统初始用户", "后台超级管理员" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remark",
                table: "Sys_User");

            migrationBuilder.AlterColumn<string>(
                name: "Sign",
                table: "Sys_User",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "个人签名",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "个人签名");

            migrationBuilder.AlterColumn<string>(
                name: "Avatar",
                table: "Sys_User",
                type: "TEXT",
                maxLength: 2000,
                nullable: false,
                defaultValue: "",
                comment: "头像",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 2000,
                oldNullable: true,
                oldComment: "头像");

            migrationBuilder.UpdateData(
                table: "Sys_Menu",
                keyColumn: "Id",
                keyValue: "100",
                columns: new[] { "Icon", "Status" },
                values: new object[] { "", null });

            migrationBuilder.UpdateData(
                table: "Sys_Menu",
                keyColumn: "Id",
                keyValue: "900",
                column: "Status",
                value: null);

            migrationBuilder.UpdateData(
                table: "Sys_User",
                keyColumn: "Id",
                keyValue: "1",
                column: "Sign",
                value: "后台管理");
        }
    }
}
