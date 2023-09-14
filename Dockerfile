FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CognitoAuthAPI/CognitoAuthAPI.csproj", "CognitoAuthAPI/"]
COPY ["CognitoAuthAPI.BLL/CognitoAuthAPI.BLL.csproj", "CognitoAuthAPI.BLL/"]
COPY ["CognitoAuthAPI.Model/CognitoAuthAPI.Model.csproj", "CognitoAuthAPI.Model/"]
RUN dotnet restore "CognitoAuthAPI/CognitoAuthAPI.csproj"
COPY . .
WORKDIR "/src/CognitoAuthAPI"
RUN dotnet build "CognitoAuthAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CognitoAuthAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CognitoAuthAPI.dll"]