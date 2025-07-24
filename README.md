# Customer Management System

A modular .NET Core 8 customer management system with a beautiful MudBlazor UI, built using clean architecture principles.

## 📋 Overview

This Customer Management System provides a robust solution for managing customer information. Built with modern architecture principles and technologies, it offers a scalable and maintainable approach to customer relationship management.

## 🏗️ Architecture

The project follows Clean Architecture principles with a modular structure:

- **CustomerManagement.Core**: Domain entities and repository interfaces
- **CustomerManagement.Application**: Business logic using CQRS with MediatR
- **CustomerManagement.Infrastructure**: Data access using Entity Framework Core
- **CustomerManagement.API**: RESTful API endpoints
- **CustomerManagement.UI**: Blazor WebAssembly frontend with MudBlazor
- **CustomerManagement.Shared**: DTOs shared between projects

## ✨ Features

- **Customer Management**: Full CRUD operations for customer data
- **Search Capabilities**: Search customers by name, status, and type
- **Responsive UI**: Beautiful and modern UI built with MudBlazor components
- **Validation**: Request validation using FluentValidation
- **Data Seeding**: Initial data from CSV files

## 🚀 Technologies

- **.NET Core 8**: Modern, cross-platform framework
- **Entity Framework Core**: ORM for data access
- **MediatR**: Implementation of CQRS pattern
- **AutoMapper**: Object-to-object mapping
- **FluentValidation**: Elegant validation for .NET
- **Blazor WebAssembly**: Client-side web UI framework
- **MudBlazor**: Material Design components for Blazor
- **SQL Server**: Database (configurable to use LocalDB for development)

## 🛠️ Getting Started

### Prerequisites

- .NET 8 SDK
- Visual Studio 2022, Visual Studio Code, or JetBrains Rider
- SQL Server or SQL Server LocalDB
- Azure CLI (for Azure deployments)

### Installation

1. Clone the repository:
   ```
   git clone https://github.com/RobertoBorges/CustomerManagement.git
   cd CustomerManagement
   ```

2. Restore NuGet packages:
   ```
   dotnet restore
   ```

3. Update the connection string in `CustomerManagement.Api/appsettings.json` if needed

4. Build the solution:
   
   ```bash
   dotnet build
   ```

5. Run the projects:
   
   - API: 
     
     ```bash
     cd CustomerManagement.Api
     dotnet run
     ```
   
   - UI: 
     
     ```bash
     cd CustomerManagement.UI
     dotnet run
     ```

### Using VS Code

This repository includes VS Code configuration files for debugging:

1. Open the solution in VS Code
2. Select the "Full Stack: API + UI" configuration from the Run and Debug panel
3. Press F5 or click the green arrow to start debugging both projects

## 🚀 Azure Deployment

This project can be deployed to Azure using Bicep templates and GitHub Actions workflows.

### Deployment Options

1. **Manual Deployment**: Use the PowerShell script in `deploy/azure` folder
   
   ```powershell
   cd deploy/azure
   ./deploy.ps1
   ```

2. **Automated CI/CD**: Use the GitHub Actions workflow in `.github/workflows/deploy-azure.yml`

See [AZURE_DEPLOYMENT.md](docs/AZURE_DEPLOYMENT.md) for detailed instructions.

## 📊 Data Structure

The customer data includes:

- Personal information (name, email, phone, address)
- Business information (company, customer type)
- Financial details (payment method)
- Additional metadata (registration date, status, notes)

## 🌐 API Endpoints

| Method | Endpoint                    | Description                  |
|--------|----------------------------|------------------------------|
| GET    | /api/customers             | Get all customers            |
| GET    | /api/customers/{id}        | Get customer by ID           |
| GET    | /api/customers/search      | Search customers by name     |
| GET    | /api/customers/status/{status} | Get customers by status  |
| GET    | /api/customers/type/{type} | Get customers by type        |
| POST   | /api/customers             | Create new customer          |
| PUT    | /api/customers/{id}        | Update existing customer     |
| DELETE | /api/customers/{id}        | Delete customer              |

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

Copyright (c) 2025 Roberto Borges

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request
