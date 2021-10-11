#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["FeshShop.Identity/src/FeshShop.Identity/FeshShop.Identity.csproj", "FeshShop.Identity/src/FeshShop.Identity/"]
COPY ["FeshShop.Common/src/FeshShop.Common/FeshShop.Common.csproj", "FeshShop.Common/src/FeshShop.Common/"]
RUN dotnet restore "FeshShop.Identity/src/FeshShop.Identity/FeshShop.Identity.csproj"
COPY . .
WORKDIR "/src/FeshShop.Identity/src/FeshShop.Identity"
RUN dotnet build "FeshShop.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FeshShop.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FeshShop.Identity.dll"]