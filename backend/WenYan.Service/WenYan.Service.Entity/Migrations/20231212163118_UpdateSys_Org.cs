using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WenYan.Service.Entity.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSys_Org : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "Sys_Org",
                type: "TEXT",
                maxLength: 256,
                nullable: true,
                comment: "描述");

            migrationBuilder.AddColumn<int>(
                name: "Seq",
                table: "Sys_Org",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                comment: "排序");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Sys_Org",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "状态");

            migrationBuilder.UpdateData(
                table: "Sys_Org",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "Remark", "Status" },
                values: new object[] { null, "Enable" });

            migrationBuilder.UpdateData(
                table: "Sys_Org",
                keyColumn: "Id",
                keyValue: "12",
                columns: new[] { "Remark", "Status" },
                values: new object[] { null, "Enable" });

            migrationBuilder.UpdateData(
                table: "Sys_Org",
                keyColumn: "Id",
                keyValue: "121",
                columns: new[] { "Remark", "Status" },
                values: new object[] { null, "Enable" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remark",
                table: "Sys_Org");

            migrationBuilder.DropColumn(
                name: "Seq",
                table: "Sys_Org");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Sys_Org");
        }
    }
}
