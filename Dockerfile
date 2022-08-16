FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 80/tcp

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["user-api.csproj", "./"]
RUN dotnet restore "user-api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "user-api.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "user-api.csproj" -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "user-api.dll"]