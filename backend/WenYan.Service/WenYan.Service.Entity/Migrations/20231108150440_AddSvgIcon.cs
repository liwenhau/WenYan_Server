using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WenYan.Service.Entity.Migrations
{
    /// <inheritdoc />
    public partial class AddSvgIcon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SvgIcon",
                table: "Sys_Menu",
                type: "TEXT",
                maxLength: 100,
                nullable: true,
                comment: "Svg图标");

            migrationBuilder.UpdateData(
                table: "Sys_Menu",
                keyColumn: "Id",
                keyValue: "100",
                columns: new[] { "Icon", "SvgIcon" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Sys_Menu",
                keyColumn: "Id",
                keyValue: "101",
                columns: new[] { "Icon", "SvgIcon" },
                values: new object[] { "", "menu-system" });

            migrationBuilder.UpdateData(
                table: "Sys_Menu",
                keyColumn: "Id",
                keyValue: "900",
                columns: new[] { "Icon", "SvgIcon" },
                values: new object[] { "", "menu-system" });

            migrationBuilder.UpdateData(
                table: "Sys_Menu",
                keyColumn: "Id",
                keyValue: "901",
                column: "SvgIcon",
                value: "");

            migrationBuilder.UpdateData(
                table: "Sys_Menu",
                keyColumn: "Id",
                keyValue: "9010",
                column: "SvgIcon",
                value: null);

            migrationBuilder.UpdateData(
                table: "Sys_Menu",
                keyColumn: "Id",
                keyValue: "9011",
                column: "SvgIcon",
                value: null);

            migrationBuilder.UpdateData(
                table: "Sys_Menu",
                keyColumn: "Id",
                keyValue: "9012",
                column: "SvgIcon",
                value: null);

            migrationBuilder.UpdateData(
                table: "Sys_Menu",
                keyColumn: "Id",
                keyValue: "9013",
                column: "SvgIcon",
                value: null);

            migrationBuilder.UpdateData(
                table: "Sys_Menu",
                keyColumn: "Id",
                keyValue: "902",
                column: "SvgIcon",
                value: "");

            migrationBuilder.UpdateData(
                table: "Sys_Menu",
                keyColumn: "Id",
                keyValue: "903",
                column: "SvgIcon",
                value: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SvgIcon",
                table: "Sys_Menu");

            migrationBuilder.UpdateData(
                table: "Sys_Menu",
                keyColumn: "Id",
                keyValue: "100",
                column: "Icon",
                value: "menu-chart");

            migrationBuilder.UpdateData(
                table: "Sys_Menu",
                keyColumn: "Id",
                keyValue: "101",
                column: "Icon",
                value: "menu-chart");

            migrationBuilder.UpdateData(
                table: "Sys_Menu",
                keyColumn: "Id",
                keyValue: "900",
                column: "Icon",
                value: "menu-system");
        }
    }
}
