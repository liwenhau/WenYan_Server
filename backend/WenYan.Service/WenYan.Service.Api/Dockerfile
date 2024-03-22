#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
# 注释掉 USER app，这句话的意思是我们的docker镜像中要有用户名app,这样可以避免一些安全问题，但是需要额外添加用户app
#USER app
WORKDIR /app
EXPOSE 80

#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#COPY ["WenYan.Service.Api/WenYan.Service.Api.csproj", "WenYan.Service.Api/"]
#COPY ["WenYan.Service.IBusiness/WenYan.Service.IBusiness.csproj", "WenYan.Service.IBusiness/"]
#COPY ["WenYan.Service.Entity/WenYan.Service.Entity.csproj", "WenYan.Service.Entity/"]
#COPY ["WenYan.Service.Util/WenYan.Service.Util.csproj", "WenYan.Service.Util/"]
#COPY ["WenYan.Service.Business/WenYan.Service.Business.csproj", "WenYan.Service.Business/"]
#RUN dotnet restore "./WenYan.Service.Api/./WenYan.Service.Api.csproj"
COPY . .
#WORKDIR "/src/WenYan.Service.Api"
#RUN dotnet build "./WenYan.Service.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build
#
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "./WenYan.Service.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
# 添加时区环境变量，亚洲，上海
ENV TimeZone=Asia/Shanghai
# 使用软连接，并且将时区配置覆盖/etc/timezone
RUN ln -snf /usr/share/zoneinfo/$TimeZone /etc/localtime && echo $TimeZone > /etc/timezone
ENTRYPOINT ["dotnet", "WenYan.Service.Api.dll"]