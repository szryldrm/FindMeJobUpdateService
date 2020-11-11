#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["FindMeJobUpdateService.WebAPI/FindMeJobUpdateService.WebAPI.csproj", "FindMeJobUpdateService.WebAPI/"]
COPY ["FindMeJobUpdateService.Core/FindMeJobUpdateService.Core.csproj", "FindMeJobUpdateService.Core/"]
COPY ["FindMeJobUpdateService.Business/FindMeJobUpdateService.Business.csproj", "FindMeJobUpdateService.Business/"]
COPY ["FindMeJobUpdateService.DataAccess/FindMeJobUpdateService.DataAccess.csproj", "FindMeJobUpdateService.DataAccess/"]
COPY ["FindMeJobUpdateService.Entities/FindMeJobUpdateService.Entities.csproj", "FindMeJobUpdateService.Entities/"]
RUN dotnet restore "FindMeJobUpdateService.WebAPI/FindMeJobUpdateService.WebAPI.csproj"
COPY . .
WORKDIR "/src/FindMeJobUpdateService.WebAPI"
RUN dotnet build "FindMeJobUpdateService.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FindMeJobUpdateService.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://*:4550
ENTRYPOINT ["dotnet", "FindMeJobUpdateService.WebAPI.dll"]