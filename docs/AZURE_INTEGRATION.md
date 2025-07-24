# Azure Integration

This document describes the Azure deployment infrastructure added to the Customer Management System.

## Added Resources

1. **Bicep Infrastructure Templates**:
   - `deploy/azure/main.bicep`: Main Bicep template that defines all Azure resources
   - `deploy/azure/main.parameters.json`: Parameter file template for deployments

2. **Deployment Scripts**:
   - `deploy/azure/deploy.ps1`: PowerShell script to deploy resources to Azure

3. **GitHub Actions Workflows**:
   - `.github/workflows/build-test.yml`: Build and test on push/PR
   - `.github/workflows/deploy-azure.yml`: Deploy to Azure environments
   - `.github/workflows/pr-validation.yml`: Validate PRs including Bicep files

4. **Azure Developer CLI (azd) Configuration**:
   - `azure.yaml`: Configuration file for Azure Developer CLI
   - `.azd/local/docker-compose.yaml`: Local development environment

5. **Documentation**:
   - `docs/AZURE_DEPLOYMENT.md`: Detailed Azure deployment guide

## Azure Resources

The following Azure resources are provisioned:

1. **App Service Plan**: Hosts web applications
2. **App Services**: Separate services for API and UI components
3. **SQL Server and Database**: For storing customer data
4. **Key Vault**: Secure storage of secrets and connection strings
5. **Application Insights**: For monitoring and telemetry
6. **Log Analytics Workspace**: For log aggregation and analysis
7. **Storage Account**: For general storage needs

## Authentication & Security

- System-assigned Managed Identities for App Services
- Key Vault for secret management
- HTTPS enforced on all endpoints
- SQL Server firewall rules properly configured
- Least-privilege access controls

## Deployment Methods

1. **Manual Deployment**:
   - Using PowerShell script in `deploy/azure` folder
   - Supports parameter customization

2. **CI/CD with GitHub Actions**:
   - Automated build, test, and deployment
   - Environment-specific deployments (dev, test, prod)
   - Database migrations included in deployment process

## Local Development

- Docker Compose configuration for SQL Server
- Local settings for development environment

## Monitoring & Diagnostics

- Application Insights integration
- Centralized logging with Log Analytics

## Next Steps

1. Configure Azure AD authentication (if needed)
2. Set up backup and disaster recovery
3. Implement additional security controls
4. Configure monitoring alerts
