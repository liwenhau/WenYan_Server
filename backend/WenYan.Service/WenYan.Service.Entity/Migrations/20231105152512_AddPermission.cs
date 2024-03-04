using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WenYan.Service.Entity.Migrations
{
    /// <inheritdoc />
    public partial class AddPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ModifyUserId",
                table: "Sys_User",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                comment: "修改人",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldComment: "修改人");

            migrationBuilder.AlterColumn<string>(
                name: "CreateUserId",
                table: "Sys_User",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                comment: "创建人",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldComment: "创建人");

            migrationBuilder.AlterColumn<string>(
                name: "ModifyUserId",
                table: "Sys_Org",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                comment: "修改人",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldComment: "修改人");

            migrationBuilder.AlterColumn<string>(
                name: "CreateUserId",
                table: "Sys_Org",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                comment: "创建人",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldComment: "创建人");

            migrationBuilder.CreateTable(
                name: "Sys_Menu",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false, comment: "主键"),
                    ParentId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true, comment: "上级菜单"),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false, comment: "名称"),
                    Code = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false, comment: "编码"),
                    Type = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false, comment: "菜单类型"),
                    Status = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true, comment: "状态"),
                    Icon = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true, comment: "图标"),
                    Path = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true, comment: "路径"),
                    Component = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true, comment: "组件"),
                    Redirect = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true, comment: "跳转"),
                    Permission = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true, comment: "权限标识"),
                    Seq = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0, comment: "排序"),
                    OutLink = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true, comment: "外链链接"),
                    IsHide = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false, comment: "是否隐藏"),
                    IsKeepAlive = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false, comment: "是否缓存"),
                    IsAffix = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false, comment: "是否固定"),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false, comment: "是否删除"),
                    CreateUserId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true, comment: "创建人"),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false, comment: "创建时间"),
                    ModifyUserId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true, comment: "修改人"),
                    ModifyTime = table.Column<DateTime>(type: "TEXT", nullable: false, comment: "修改时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Menu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sys_Menu_Sys_Menu_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Sys_Menu",
                        principalColumn: "Id");
                },
                comment: "菜单");

            migrationBuilder.CreateTable(
                name: "Sys_Role",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false, comment: "主键"),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false, comment: "角色名称"),
                    Code = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false, comment: "角色编码"),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false, comment: "是否删除"),
                    CreateUserId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true, comment: "创建人"),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false, comment: "创建时间"),
                    ModifyUserId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true, comment: "修改人"),
                    ModifyTime = table.Column<DateTime>(type: "TEXT", nullable: false, comment: "修改时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Role", x => x.Id);
                },
                comment: "角色");

            migrationBuilder.CreateTable(
                name: "Sys_UserRole",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Id = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreateUserId = table.Column<string>(type: "TEXT", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifyUserId = table.Column<string>(type: "TEXT", nullable: true),
                    ModifyTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_Sys_UserRole_Sys_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Sys_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sys_UserRole_Sys_User_UserId",
                        column: x => x.UserId,
                        principalTable: "Sys_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "用户角色");

            migrationBuilder.InsertData(
                table: "Sys_Menu",
                columns: new[] { "Id", "Code", "Component", "CreateTime", "CreateUserId", "Icon", "ModifyTime", "ModifyUserId", "Name", "OutLink", "ParentId", "Path", "Permission", "Redirect", "Seq", "Status", "Type" },
                values: new object[,]
                {
                    { "100", "Analyse", "Layout", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1", "分析页", null, null, "/analyse", null, "analyse/index", 2, null, "1" },
                    { "900", "Sys", "Layout", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1", "menu-system", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1", "系统管理", null, null, "/system", null, "system/user/index", 9, null, "1" }
                });

            migrationBuilder.InsertData(
                table: "Sys_Role",
                columns: new[] { "Id", "Code", "CreateTime", "CreateUserId", "ModifyTime", "ModifyUserId", "Name" },
                values: new object[,]
                {
                    { "1", "Admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1", "系统管理员" },
                    { "2", "User", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1", "普通用户" }
                });

            migrationBuilder.InsertData(
                table: "Sys_Menu",
                columns: new[] { "Id", "Code", "Component", "CreateTime", "CreateUserId", "Icon", "ModifyTime", "ModifyUserId", "Name", "OutLink", "ParentId", "Path", "Permission", "Redirect", "Seq", "Status", "Type" },
                values: new object[,]
                {
                    { "101", "AnalyseIndex", "analyse/index", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1", "menu-chart", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1", "分析页", null, "100", "/analyse/index", null, null, 1, "Enable", "2" },
                    { "901", "Sys_User", "system/user/index", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1", "icon-user", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1", "用户管理", null, "900", "/system/user", null, null, 1, "Enable", "2" },
                    { "902", "Sys_Role", "system/role/index", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1", "icon-common", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1", "角色管理", null, "900", "/system/role", null, null, 2, "Enable", "2" },
                    { "903", "Sys_Menu", "system/menu/index", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1", "icon-menu", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1", "菜单管理", null, "900", "/system/menu", null, null, 3, "Enable", "2" }
                });

            migrationBuilder.InsertData(
                table: "Sys_UserRole",
                columns: new[] { "RoleId", "UserId", "CreateTime", "CreateUserId", "Deleted", "Id", "ModifyTime", "ModifyUserId" },
                values: new object[] { "1", "1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.InsertData(
                table: "Sys_Menu",
                columns: new[] { "Id", "Code", "Component", "CreateTime", "CreateUserId", "Icon", "ModifyTime", "ModifyUserId", "Name", "OutLink", "ParentId", "Path", "Permission", "Redirect", "Status", "Type" },
                values: new object[,]
                {
                    { "9010", "Sys_User_Add", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1", "新增", null, "901", null, "user:btn.add", null, "Enable", "3" },
                    { "9011", "Sys_User_Update", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1", "修改", null, "901", null, "user:btn.update", null, "Enable", "3" },
                    { "9012", "Sys_User_Delete", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1", "删除", null, "901", null, "user:btn.delete", null, "Enable", "3" },
                    { "9013", "Sys_User_Query", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1", "查询", null, "901", null, "user:btn.query", null, "Enable", "3" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sys_Menu_Deleted",
                table: "Sys_Menu",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_Sys_Menu_ParentId",
                table: "Sys_Menu",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Sys_Role_Deleted",
                table: "Sys_Role",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_Sys_UserRole_RoleId",
                table: "Sys_UserRole",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sys_Menu");

            migrationBuilder.DropTable(
                name: "Sys_UserRole");

            migrationBuilder.DropTable(
                name: "Sys_Role");

            migrationBuilder.AlterColumn<string>(
                name: "ModifyUserId",
                table: "Sys_User",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "修改人",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "修改人");

            migrationBuilder.AlterColumn<string>(
                name: "CreateUserId",
                table: "Sys_User",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "创建人",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "创建人");

            migrationBuilder.AlterColumn<string>(
                name: "ModifyUserId",
                table: "Sys_Org",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "修改人",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "修改人");

            migrationBuilder.AlterColumn<string>(
                name: "CreateUserId",
                table: "Sys_Org",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "创建人",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "创建人");
        }
    }
}
