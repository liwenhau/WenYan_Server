﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WenYan.Service.Entity;

#nullable disable

namespace WenYan.Service.Entity.Migrations
{
    [DbContext(typeof(GDbContext))]
    [Migration("20231105154321_AddRoleMenu")]
    partial class AddRoleMenu
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.11");

            modelBuilder.Entity("WenYan.Service.Entity.Sys_Menu", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasComment("主键");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasComment("编码");

                    b.Property<string>("Component")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasComment("组件");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("TEXT")
                        .HasComment("创建时间");

                    b.Property<string>("CreateUserId")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasComment("创建人");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false)
                        .HasComment("是否删除");

                    b.Property<string>("Icon")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasComment("图标");

                    b.Property<bool>("IsAffix")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false)
                        .HasComment("是否固定");

                    b.Property<bool>("IsHide")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false)
                        .HasComment("是否隐藏");

                    b.Property<bool>("IsKeepAlive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false)
                        .HasComment("是否缓存");

                    b.Property<DateTime>("ModifyTime")
                        .HasColumnType("TEXT")
                        .HasComment("修改时间");

                    b.Property<string>("ModifyUserId")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasComment("修改人");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasComment("名称");

                    b.Property<string>("OutLink")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT")
                        .HasComment("外链链接");

                    b.Property<string>("ParentId")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasComment("上级菜单");

                    b.Property<string>("Path")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasComment("路径");

                    b.Property<string>("Permission")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasComment("权限标识");

                    b.Property<string>("Redirect")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasComment("跳转");

                    b.Property<int>("Seq")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(0)
                        .HasComment("排序");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasComment("状态");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasComment("菜单类型");

                    b.HasKey("Id");

                    b.HasIndex("Deleted");

                    b.HasIndex("ParentId");

                    b.ToTable("Sys_Menu", t =>
                        {
                            t.HasComment("菜单");
                        });

                    b.HasData(
                        new
                        {
                            Id = "100",
                            Code = "Analyse",
                            Component = "Layout",
                            CreateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreateUserId = "1",
                            Deleted = false,
                            Icon = "",
                            IsAffix = false,
                            IsHide = false,
                            IsKeepAlive = false,
                            ModifyTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifyUserId = "1",
                            Name = "分析页",
                            Path = "/analyse",
                            Redirect = "analyse/index",
                            Seq = 2,
                            Type = "1"
                        },
                        new
                        {
                            Id = "101",
                            Code = "AnalyseIndex",
                            Component = "analyse/index",
                            CreateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreateUserId = "1",
                            Deleted = false,
                            Icon = "menu-chart",
                            IsAffix = false,
                            IsHide = false,
                            IsKeepAlive = false,
                            ModifyTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifyUserId = "1",
                            Name = "分析页",
                            ParentId = "100",
                            Path = "/analyse/index",
                            Seq = 1,
                            Status = "Enable",
                            Type = "2"
                        },
                        new
                        {
                            Id = "900",
                            Code = "Sys",
                            Component = "Layout",
                            CreateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreateUserId = "1",
                            Deleted = false,
                            Icon = "menu-system",
                            IsAffix = false,
                            IsHide = false,
                            IsKeepAlive = false,
                            ModifyTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifyUserId = "1",
                            Name = "系统管理",
                            Path = "/system",
                            Redirect = "system/user/index",
                            Seq = 9,
                            Type = "1"
                        },
                        new
                        {
                            Id = "901",
                            Code = "Sys_User",
                            Component = "system/user/index",
                            CreateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreateUserId = "1",
                            Deleted = false,
                            Icon = "icon-user",
                            IsAffix = false,
                            IsHide = false,
                            IsKeepAlive = false,
                            ModifyTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifyUserId = "1",
                            Name = "用户管理",
                            ParentId = "900",
                            Path = "/system/user",
                            Seq = 1,
                            Status = "Enable",
                            Type = "2"
                        },
                        new
                        {
                            Id = "902",
                            Code = "Sys_Role",
                            Component = "system/role/index",
                            CreateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreateUserId = "1",
                            Deleted = false,
                            Icon = "icon-common",
                            IsAffix = false,
                            IsHide = false,
                            IsKeepAlive = false,
                            ModifyTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifyUserId = "1",
                            Name = "角色管理",
                            ParentId = "900",
                            Path = "/system/role",
                            Seq = 2,
                            Status = "Enable",
                            Type = "2"
                        },
                        new
                        {
                            Id = "903",
                            Code = "Sys_Menu",
                            Component = "system/menu/index",
                            CreateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreateUserId = "1",
                            Deleted = false,
                            Icon = "icon-menu",
                            IsAffix = false,
                            IsHide = false,
                            IsKeepAlive = false,
                            ModifyTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifyUserId = "1",
                            Name = "菜单管理",
                            ParentId = "900",
                            Path = "/system/menu",
                            Seq = 3,
                            Status = "Enable",
                            Type = "2"
                        },
                        new
                        {
                            Id = "9010",
                            Code = "Sys_User_Add",
                            CreateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreateUserId = "1",
                            Deleted = false,
                            IsAffix = false,
                            IsHide = false,
                            IsKeepAlive = false,
                            ModifyTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifyUserId = "1",
                            Name = "新增",
                            ParentId = "901",
                            Permission = "user:btn.add",
                            Seq = 0,
                            Status = "Enable",
                            Type = "3"
                        },
                        new
                        {
                            Id = "9011",
                            Code = "Sys_User_Update",
                            CreateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreateUserId = "1",
                            Deleted = false,
                            IsAffix = false,
                            IsHide = false,
                            IsKeepAlive = false,
                            ModifyTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifyUserId = "1",
                            Name = "修改",
                            ParentId = "901",
                            Permission = "user:btn.update",
                            Seq = 0,
                            Status = "Enable",
                            Type = "3"
                        },
                        new
                        {
                            Id = "9012",
                            Code = "Sys_User_Delete",
                            CreateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreateUserId = "1",
                            Deleted = false,
                            IsAffix = false,
                            IsHide = false,
                            IsKeepAlive = false,
                            ModifyTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifyUserId = "1",
                            Name = "删除",
                            ParentId = "901",
                            Permission = "user:btn.delete",
                            Seq = 0,
                            Status = "Enable",
                            Type = "3"
                        },
                        new
                        {
                            Id = "9013",
                            Code = "Sys_User_Query",
                            CreateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreateUserId = "1",
                            Deleted = false,
                            IsAffix = false,
                            IsHide = false,
                            IsKeepAlive = false,
                            ModifyTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifyUserId = "1",
                            Name = "查询",
                            ParentId = "901",
                            Permission = "user:btn.query",
                            Seq = 0,
                            Status = "Enable",
                            Type = "3"
                        });
                });

            modelBuilder.Entity("WenYan.Service.Entity.Sys_Org", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasComment("主键");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasComment("编码");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("TEXT")
                        .HasComment("创建时间");

                    b.Property<string>("CreateUserId")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasComment("创建人");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false)
                        .HasComment("是否删除");

                    b.Property<DateTime>("ModifyTime")
                        .HasColumnType("TEXT")
                        .HasComment("修改时间");

                    b.Property<string>("ModifyUserId")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasComment("修改人");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasComment("名称");

                    b.Property<string>("ParentId")
                        .HasColumnType("TEXT")
                        .HasComment("上级组织");

                    b.HasKey("Id");

                    b.HasIndex("Deleted");

                    b.HasIndex("ParentId");

                    b.ToTable("Sys_Org", t =>
                        {
                            t.HasComment("组织架构");
                        });

                    b.HasData(
                        new
                        {
                            Id = "1",
                            Code = "Group",
                            CreateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreateUserId = "1",
                            Deleted = false,
                            ModifyTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifyUserId = "1",
                            Name = "公司"
                        },
                        new
                        {
                            Id = "12",
                            Code = "Company",
                            CreateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreateUserId = "1",
                            Deleted = false,
                            ModifyTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifyUserId = "1",
                            Name = "部门",
                            ParentId = "1"
                        },
                        new
                        {
                            Id = "121",
                            Code = "Dept",
                            CreateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreateUserId = "1",
                            Deleted = false,
                            ModifyTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifyUserId = "1",
                            Name = "子部门",
                            ParentId = "12"
                        });
                });

            modelBuilder.Entity("WenYan.Service.Entity.Sys_Role", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasComment("主键");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasComment("角色编码");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("TEXT")
                        .HasComment("创建时间");

                    b.Property<string>("CreateUserId")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasComment("创建人");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false)
                        .HasComment("是否删除");

                    b.Property<DateTime>("ModifyTime")
                        .HasColumnType("TEXT")
                        .HasComment("修改时间");

                    b.Property<string>("ModifyUserId")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasComment("修改人");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasComment("角色名称");

                    b.HasKey("Id");

                    b.HasIndex("Deleted");

                    b.ToTable("Sys_Role", t =>
                        {
                            t.HasComment("角色");
                        });

                    b.HasData(
                        new
                        {
                            Id = "1",
                            Code = "Admin",
                            CreateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreateUserId = "1",
                            Deleted = false,
                            ModifyTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifyUserId = "1",
                            Name = "系统管理员"
                        },
                        new
                        {
                            Id = "2",
                            Code = "User",
                            CreateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreateUserId = "1",
                            Deleted = false,
                            ModifyTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifyUserId = "1",
                            Name = "普通用户"
                        });
                });

            modelBuilder.Entity("WenYan.Service.Entity.Sys_RoleMenu", b =>
                {
                    b.Property<string>("MenuId")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("MenuId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("Sys_RoleMenu", t =>
                        {
                            t.HasComment("角色菜单");
                        });

                    b.HasData(
                        new
                        {
                            MenuId = "100",
                            RoleId = "1"
                        },
                        new
                        {
                            MenuId = "101",
                            RoleId = "1"
                        },
                        new
                        {
                            MenuId = "900",
                            RoleId = "1"
                        },
                        new
                        {
                            MenuId = "901",
                            RoleId = "1"
                        },
                        new
                        {
                            MenuId = "9010",
                            RoleId = "1"
                        },
                        new
                        {
                            MenuId = "9011",
                            RoleId = "1"
                        },
                        new
                        {
                            MenuId = "9012",
                            RoleId = "1"
                        },
                        new
                        {
                            MenuId = "9013",
                            RoleId = "1"
                        },
                        new
                        {
                            MenuId = "902",
                            RoleId = "1"
                        },
                        new
                        {
                            MenuId = "903",
                            RoleId = "1"
                        });
                });

            modelBuilder.Entity("WenYan.Service.Entity.Sys_User", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasComment("主键");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasComment("编码");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("TEXT")
                        .HasComment("创建时间");

                    b.Property<string>("CreateUserId")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasComment("创建人");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false)
                        .HasComment("是否删除");

                    b.Property<string>("HeadPortrait")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("TEXT")
                        .HasComment("头像");

                    b.Property<DateTime>("ModifyTime")
                        .HasColumnType("TEXT")
                        .HasComment("修改时间");

                    b.Property<string>("ModifyUserId")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasComment("修改人");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasComment("名称");

                    b.Property<string>("OrgId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasComment("所属组织");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasComment("密码");

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasComment("性别");

                    b.Property<string>("Sign")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasComment("个人签名");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasComment("状态");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasComment("用户名");

                    b.HasKey("Id");

                    b.HasIndex("Deleted");

                    b.HasIndex("OrgId");

                    b.ToTable("Sys_User", t =>
                        {
                            t.HasComment("系统用户");
                        });

                    b.HasData(
                        new
                        {
                            Id = "1",
                            Code = "U0000",
                            CreateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreateUserId = "1",
                            Deleted = false,
                            HeadPortrait = "",
                            ModifyTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifyUserId = "1",
                            Name = "Admin",
                            OrgId = "1",
                            Password = "1a50207b5dc3aade372204b6169bd01a",
                            Sex = "Boy",
                            Sign = "后台管理",
                            Status = "Enable",
                            UserName = "admin"
                        });
                });

            modelBuilder.Entity("WenYan.Service.Entity.Sys_UserRole", b =>
                {
                    b.Property<string>("UserId")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("Sys_UserRole", t =>
                        {
                            t.HasComment("用户角色");
                        });

                    b.HasData(
                        new
                        {
                            UserId = "1",
                            RoleId = "1"
                        });
                });

            modelBuilder.Entity("WenYan.Service.Entity.Sys_Menu", b =>
                {
                    b.HasOne("WenYan.Service.Entity.Sys_Menu", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("WenYan.Service.Entity.Sys_Org", b =>
                {
                    b.HasOne("WenYan.Service.Entity.Sys_Org", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("WenYan.Service.Entity.Sys_RoleMenu", b =>
                {
                    b.HasOne("WenYan.Service.Entity.Sys_Menu", "Menu")
                        .WithMany()
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WenYan.Service.Entity.Sys_Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Menu");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("WenYan.Service.Entity.Sys_User", b =>
                {
                    b.HasOne("WenYan.Service.Entity.Sys_Org", "Org")
                        .WithMany()
                        .HasForeignKey("OrgId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Org");
                });

            modelBuilder.Entity("WenYan.Service.Entity.Sys_UserRole", b =>
                {
                    b.HasOne("WenYan.Service.Entity.Sys_Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WenYan.Service.Entity.Sys_User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WenYan.Service.Entity.Sys_Menu", b =>
                {
                    b.Navigation("Children");
                });

            modelBuilder.Entity("WenYan.Service.Entity.Sys_Org", b =>
                {
                    b.Navigation("Children");
                });
#pragma warning restore 612, 618
        }
    }
}
