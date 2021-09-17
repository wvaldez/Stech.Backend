FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 55623

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Stech.Backend.API/Stech.Backend.API.csproj", "Stech.Backend.API/"]
COPY ["Stech.Backend.Core/Stech.Backend.Core.csproj", "Stech.Backend.Core/"]
COPY ["Stech.Backend.Data/Stech.Backend.Data.csproj", "Stech.Backend.Data/"]

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