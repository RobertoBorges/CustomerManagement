FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["CustomerManagement.UI/CustomerManagement.UI.csproj", "CustomerManagement.UI/"]
COPY ["CustomerManagement.Shared/CustomerManagement.Shared.csproj", "CustomerManagement.Shared/"]
RUN dotnet restore "CustomerManagement.UI/CustomerManagement.UI.csproj"
COPY . .
WORKDIR "/src/CustomerManagement.UI"
# Create Docker-specific appsettings.json
RUN echo '{"ApiBaseUrl": "http://localhost:5000","Logging": {"LogLevel": {"Default": "Information","Microsoft.AspNetCore": "Warning"}}}' > wwwroot/appsettings.json
RUN dotnet build "CustomerManagement.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CustomerManagement.UI.csproj" -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY CustomerManagement.UI/nginx.conf /etc/nginx/nginx.conf
EXPOSE 80
EXPOSE 443
