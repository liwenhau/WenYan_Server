using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WenYan.Service.Entity.Migrations
{
    /// <inheritdoc />
    public partial class AddInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sys_Org",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false, comment: "主键"),
                    ParentId = table.Column<string>(type: "TEXT", nullable: true, comment: "上级组织"),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false, comment: "名称"),
                    Code = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false, comment: "编码"),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false, comment: "是否删除"),
                    CreateUserId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false, comment: "创建人"),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false, comment: "创建时间"),
                    ModifyUserId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false, comment: "修改人"),
                    ModifyTime = table.Column<DateTime>(type: "TEXT", nullable: false, comment: "修改时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Org", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sys_Org_Sys_Org_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Sys_Org",
                        principalColumn: "Id");
                },
                comment: "组织架构");

            migrationBuilder.CreateTable(
                name: "Sys_User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false, comment: "主键"),
                    Code = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false, comment: "编码"),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false, comment: "名称"),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false, comment: "用户名"),
                    Password = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false, comment: "密码"),
                    Status = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false, comment: "状态"),
                    OrgId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false, comment: "所属组织"),
                    Sex = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false, comment: "性别"),
                    HeadPortrait = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false, comment: "头像"),
                    Sign = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false, comment: "个人签名"),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false, comment: "是否删除"),
                    CreateUserId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false, comment: "创建人"),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false, comment: "创建时间"),
                    ModifyUserId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false, comment: "修改人"),
                    ModifyTime = table.Column<DateTime>(type: "TEXT", nullable: false, comment: "修改时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sys_User_Sys_Org_OrgId",
                        column: x => x.OrgId,
                        principalTable: "Sys_Org",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "系统用户");

            migrationBuilder.InsertData(
                table: "Sys_Org",
                columns: new[] { "Id", "Code", "CreateTime", "CreateUserId", "ModifyTime", "ModifyUserId", "Name", "ParentId" },
                values: new object[,]
                {
                    { "1", "Group", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1", "公司", null },
                    { "12", "Company", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1", "部门", "1" }
                });

            migrationBuilder.InsertData(
                table: "Sys_User",
                columns: new[] { "Id", "Code", "CreateTime", "CreateUserId", "HeadPortrait", "ModifyTime", "ModifyUserId", "Name", "OrgId", "Password", "Sex", "Sign", "Status", "UserName" },
                values: new object[] { "1", "U0000", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1", "Admin", "1", "1a50207b5dc3aade372204b6169bd01a", "Boy", "后台管理", "Enable", "admin" });

            migrationBuilder.InsertData(
                table: "Sys_Org",
                columns: new[] { "Id", "Code", "CreateTime", "CreateUserId", "ModifyTime", "ModifyUserId", "Name", "ParentId" },
                values: new object[] { "121", "Dept", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1", "子部门", "12" });

            migrationBuilder.CreateIndex(
                name: "IX_Sys_Org_Deleted",
                table: "Sys_Org",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_Sys_Org_ParentId",
                table: "Sys_Org",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Sys_User_Deleted",
                table: "Sys_User",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_Sys_User_OrgId",
                table: "Sys_User",
                column: "OrgId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sys_User");

            migrationBuilder.DropTable(
                name: "Sys_Org");
        }
    }
}
