#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ProductosWebApi/ProductosWebApi.csproj", "ProductosWebApi/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "ProductosWebApi/ProductosWebApi.csproj"
COPY . .
WORKDIR "/src/ProductosWebApi"
RUN dotnet build "ProductosWebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductosWebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductosWebApi.dll"]