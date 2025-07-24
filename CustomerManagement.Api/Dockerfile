FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["CustomerManagement.Api/CustomerManagement.Api.csproj", "CustomerManagement.Api/"]
COPY ["CustomerManagement.Core/CustomerManagement.Core.csproj", "CustomerManagement.Core/"]
COPY ["CustomerManagement.Application/CustomerManagement.Application.csproj", "CustomerManagement.Application/"]
COPY ["CustomerManagement.Infrastructure/CustomerManagement.Infrastructure.csproj", "CustomerManagement.Infrastructure/"]
COPY ["CustomerManagement.Shared/CustomerManagement.Shared.csproj", "CustomerManagement.Shared/"]
RUN dotnet restore "CustomerManagement.Api/CustomerManagement.Api.csproj"
COPY . .
WORKDIR "/src/CustomerManagement.Api"
RUN dotnet build "CustomerManagement.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CustomerManagement.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerManagement.Api.dll"]
