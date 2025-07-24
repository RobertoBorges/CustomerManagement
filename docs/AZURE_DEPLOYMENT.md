# Azure Deployment Guide

This guide provides instructions for deploying the Customer Management System to Azure.

## Prerequisites

- Azure account with an active subscription
- Azure CLI installed and configured
- PowerShell 7+ or Azure Cloud Shell

## Deployment Methods

There are two ways to deploy the application to Azure:

### 1. Using the PowerShell Deployment Script

1. Open PowerShell and navigate to the `deploy/azure` directory:

   ```powershell
   cd deploy/azure
   ```

2. Run the deployment script:

   ```powershell
   ./deploy.ps1
   ```

   Optional parameters:
   - `-SubscriptionId`: Specify Azure subscription ID
   - `-ResourceGroupName`: Override the default resource group name
   - `-Location`: Change the Azure region (default: eastus)
   - `-Environment`: Choose environment (dev, test, prod)

3. The script will:
   - Create a resource group if it doesn't exist
   - Deploy the infrastructure using Bicep templates
   - Configure Key Vault secrets
   - Output the deployment results

### 2. Using GitHub Actions

For CI/CD deployment:

1. Configure GitHub repository secrets:
   - `AZURE_CREDENTIALS`: Azure service principal credentials (JSON format)
   
   To create the service principal:
   
   ```bash
   az ad sp create-for-rbac --name "CustomerManagementCICD" --role contributor \
                           --scopes /subscriptions/<subscription-id> \
                           --sdk-auth
   ```

2. Copy the JSON output and add it as a GitHub secret named `AZURE_CREDENTIALS`

3. Trigger the deployment workflow:
   - Go to "Actions" tab in your GitHub repository
   - Select "Deploy to Azure" workflow
   - Click "Run workflow"
   - Select the target environment (dev, test, or prod)

## Azure Resources

The deployment creates the following resources:

- **App Service Plan**: Hosts the web applications
- **App Services**: Separate instances for API and UI
- **SQL Server and Database**: Stores customer data
- **Key Vault**: Securely stores credentials and configuration
- **Application Insights**: For monitoring and diagnostics
- **Storage Account**: For logs and other storage needs

## Post-Deployment Configuration

1. Configure CORS in the Azure Portal if needed:
   - Go to API App Service > API > CORS
   - Add the UI App Service URL to allowed origins

2. Update connection strings or application settings if needed:
   - Navigate to each App Service > Configuration
   - Modify environment variables or connection strings

## Monitoring

- Access Application Insights for detailed monitoring:
  - Go to Azure Portal > Application Insights resource
  - View performance metrics, failures, and usage patterns
  - Set up alerts for critical issues

## Troubleshooting

- Check App Service logs:
  - Go to App Service > Log stream
  - Or download logs from Kudu (Advanced Tools)

- Check deployment history:
  - Go to Resource Group > Deployments
  - Review any failed deployments and error messages
