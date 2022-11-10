FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
RUN apt update && \
    apt install git -y
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Apis/Apis.csproj", "Apis/"]
COPY ["Contracts/Contracts.csproj", "Contracts/"]
COPY ["DataAccess/DataAccess.csproj", "DataAccess/"]
COPY ["DataAccess.Contracts/DataAccess.Contracts.csproj", "DataAccess.Contracts/"]
COPY ["Services/Services.csproj", "Services/"]
RUN dotnet restore "Apis/Apis.csproj"
COPY . .
WORKDIR "/src/Apis"
RUN dotnet build "Apis.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Apis.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PinaryDevelopment.Git.Server.Apis.dll"]