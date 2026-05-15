# WenYan_Server

一个前后端分离的后台管理项目 ✨

[![Vue 3](https://img.shields.io/badge/Vue-3-42b883?logo=vue.js&logoColor=white)](https://cn.vuejs.org/)
[![Vite 4](https://img.shields.io/badge/Vite-4-646cff?logo=vite&logoColor=white)](https://vitejs.dev/)
[![TypeScript](https://img.shields.io/badge/TypeScript-5-3178c6?logo=typescript&logoColor=white)](https://www.typescriptlang.org/)
[![.NET 8](https://img.shields.io/badge/.NET-8-512bd4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![SQLite](https://img.shields.io/badge/SQLite-Default-003b57?logo=sqlite&logoColor=white)](https://www.sqlite.org/)
[![SignalR](https://img.shields.io/badge/SignalR-Realtime-ff6b35)](https://learn.microsoft.com/aspnet/core/signalr/introduction)

- 前端：`Vue 3 + Vite + TypeScript + Arco Design Vue`
- 后端：`ASP.NET Core 8 + EF Core + JWT + SignalR`
- 默认数据库：`SQLite`

适合拿来做后台骨架，也适合继续魔改 🛠️

## 功能

| 模块 | 简介 |
| --- | --- |
| `🔐 登录鉴权` | 支持登录、JWT、Token 刷新，基础认证流程已打通 |
| `👥 权限管理` | 包含用户、角色、菜单、组织等后台常用模块 |
| `🧭 动态路由` | 菜单由后端返回，前端按权限动态生成路由 |
| `📁 文件中心` | 支持分片上传、文件合并、文件读取和预览场景 |
| `📊 运行监控` | 通过 SignalR 推送磁盘和内存等实时监控数据 |
| `🧱 项目骨架` | 前后端基础能力齐全，适合继续扩展业务功能 |

## 目录

```text
WenYan_Server
├─ frontend
└─ backend/WenYan.Service
```

## 快速启动

### 1. 后端启动 🚀

```bash
cd backend/WenYan.Service
dotnet restore
cd WenYan.Service.Api
dotnet tool restore
dotnet ef database update --project ..\WenYan.Service.Entity\WenYan.Service.Entity.csproj --startup-project .\
dotnet run
```

- API：`http://localhost:5089`
- Swagger：`http://localhost:5089/swagger`

### 2. 前端启动 🎨

```bash
cd frontend
npm install
npm run dev
```

- 前端：`http://localhost:5173`

## 默认账号

- 用户名：`admin`
- 密码：`WenYan@2024`

## 关键配置

### 前端

文件：`frontend/.env.development`

```env
VITE_API_PREFIX='/api'
VITE_API_BASE_URL='http://localhost:5089/'
VITE_PUBLIC_PATH='/'
```

### 后端

文件：`backend/WenYan.Service/WenYan.Service.Api/appsettings.json`

- 数据库默认路径：`D:/Project/DB/WenYan.Server.db`
- 文件目录：`Files`
- 临时目录：`Temps`

如果你的本地没有这个磁盘路径，记得先改连接字符串，不然启动会扑街 😅

## 小提醒

- 首次运行前要手动执行数据库迁移
- 前端现在默认通过 `VITE_API_BASE_URL` 直连后端
- `frontend/README.md` 还是上游模板内容，以当前根目录 README 为准
- 正式部署前记得更换 JWT 密钥和数据库路径

## 一句话总结

这是一个已经具备后台基础能力的前后端项目，拉下来改改就能继续干活了 🍵

![Alt](https://repobeats.axiom.co/api/embed/2951c8c05597d125bedba32e2b2398beacbe5d10.svg "Repobeats analytics image")
