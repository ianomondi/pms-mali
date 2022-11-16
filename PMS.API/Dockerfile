FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PMS.Domain/PMS.Domain.csproj", "PMS.Domain/"]
COPY ["PMS.DAL/PMS.DAL.csproj", "PMS.DAL/"]
COPY ["PMS.Services/PMS.Services.csproj", "PMS.Services/"]
COPY ["PMS.API/PMS.API.csproj", "PMS.API/"]
RUN dotnet restore "PMS.API/PMS.API.csproj"
COPY . .
WORKDIR "/src/PMS.API"
RUN dotnet build "PMS.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN apt-get update -yq \
    && apt-get install curl gnupg -yq \
    && curl -sL https://deb.nodesource.com/setup_14.x | bash \
    && apt-get install nodejs -yq
RUN dotnet publish "PMS.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PMS.API.dll"]
