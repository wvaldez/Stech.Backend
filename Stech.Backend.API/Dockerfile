#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Stech.Backend.API/Stech.Backend.API.csproj", "Stech.Backend.API/"]
RUN dotnet restore "Stech.Backend.API/Stech.Backend.API.csproj"
COPY . .
WORKDIR "/src/Stech.Backend.API"
RUN dotnet build "Stech.Backend.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Stech.Backend.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Stech.Backend.API.dll"]