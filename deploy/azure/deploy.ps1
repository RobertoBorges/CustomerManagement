#!/usr/bin/env pwsh

param(
    [Parameter(Mandatory = $false)]
    [string]$SubscriptionId,

    [Parameter(Mandatory = $false)]
    [string]$ResourceGroupName = "rg-custmgmt-dev",

    [Parameter(Mandatory = $false)]
    [string]$Location = "eastus",

    [Parameter(Mandatory = $false)]
    [string]$Environment = "dev",
    
    [Parameter(Mandatory = $false)]
    [string]$ApplicationName = "custmgmt",

    [Parameter(Mandatory = $false)]
    [SecureString]$SqlAdminPassword,
    
    [Parameter(Mandatory = $false)]
    [switch]$CreateParameters = $false
)

$ErrorActionPreference = "Stop"

# Check if Azure CLI is installed
if (-not (Get-Command az -ErrorAction SilentlyContinue)) {
    Write-Error "Azure CLI is not installed. Please install it from https://docs.microsoft.com/en-us/cli/azure/install-azure-cli"
    exit 1
}

# Check if user is logged in to Azure
$loginStatus = az account show --output json | ConvertFrom-Json
if (-not $loginStatus) {
    Write-Host "You are not logged in to Azure. Please login..."
    az login
    $loginStatus = az account show --output json | ConvertFrom-Json
}

# Set subscription if provided
if ($SubscriptionId) {
    Write-Host "Setting subscription to $SubscriptionId..."
    az account set --subscription $SubscriptionId
}

# Display subscription details
$currentSubscription = az account show --output json | ConvertFrom-Json
Write-Host "Using subscription: $($currentSubscription.name) ($($currentSubscription.id))"

# Generate SQL admin password if not provided
if (-not $SqlAdminPassword) {
    $generatedPassword = -join ((48..57) + (65..90) + (97..122) | Get-Random -Count 16 | ForEach-Object { [char]$_ })
    $SqlAdminPassword = ConvertTo-SecureString $generatedPassword -AsPlainText -Force
    $plainTextPassword = $generatedPassword  # Store for output purposes only
    Write-Host "Generated SQL admin password"
} else {
    # We need a plain text version for the parameters file
    $bstr = [System.Runtime.InteropServices.Marshal]::SecureStringToBSTR($SqlAdminPassword)
    $plainTextPassword = [System.Runtime.InteropServices.Marshal]::PtrToStringAuto($bstr)
    [System.Runtime.InteropServices.Marshal]::ZeroFreeBSTR($bstr)
}

# Create resource group if it doesn't exist
$rgExists = az group show --name $ResourceGroupName --output json 2>$null
if (-not $rgExists) {
    Write-Host "Creating resource group $ResourceGroupName in location $Location..."
    az group create --name $ResourceGroupName --location $Location
}

# Create parameters file for deployment if needed
if ($CreateParameters) {
    $parametersTemplateFile = Join-Path $PSScriptRoot "main.parameters.json"
    $parametersFile = Join-Path $PSScriptRoot "main.parameters.$Environment.json"

    $parameters = Get-Content $parametersTemplateFile | ConvertFrom-Json
    $parameters.parameters.environmentName.value = $Environment
    $parameters.parameters.applicationName.value = $ApplicationName
    $parameters.parameters.location.value = $Location
    
    # Remove KeyVault reference for local deployment
    $parameters.parameters.sqlAdminPassword = @{
        "value" = $plainTextPassword
    }
    
    $parameters | ConvertTo-Json -Depth 10 | Set-Content $parametersFile
    
    Write-Host "Created parameters file: $parametersFile"
}

# Deploy infrastructure
$bicepFile = Join-Path $PSScriptRoot "main.bicep"
$parametersFile = Join-Path $PSScriptRoot "main.parameters.$Environment.json"

if (-not (Test-Path $parametersFile)) {
    Write-Host "Parameters file not found. Creating one..."
    $CreateParameters = $true
    $parametersTemplateFile = Join-Path $PSScriptRoot "main.parameters.json"
    $parameters = Get-Content $parametersTemplateFile | ConvertFrom-Json
    $parameters.parameters.environmentName.value = $Environment
    $parameters.parameters.applicationName.value = $ApplicationName
    $parameters.parameters.location.value = $Location
    
    # Remove KeyVault reference for local deployment
    $parameters.parameters.sqlAdminPassword = @{
        "value" = $plainTextPassword
    }
    
    $parameters | ConvertTo-Json -Depth 10 | Set-Content $parametersFile
    
    Write-Host "Created parameters file: $parametersFile"
}

Write-Host "Deploying infrastructure..."
$deployment = az deployment group create `
    --resource-group $ResourceGroupName `
    --template-file $bicepFile `
    --parameters @$parametersFile `
    --output json | ConvertFrom-Json

# Display outputs
Write-Host "`nDeployment completed successfully!`n" -ForegroundColor Green
Write-Host "Infrastructure has been provisioned with the following endpoints:"
Write-Host "API: $($deployment.properties.outputs.apiAppServiceUrl.value)"
Write-Host "UI: $($deployment.properties.outputs.uiAppServiceUrl.value)"
Write-Host "Key Vault: $($deployment.properties.outputs.keyVaultUrl.value)"
Write-Host "Application Insights Instrumentation Key: $($deployment.properties.outputs.appInsightsInstrumentationKey.value)"
Write-Host "`nSQL Server credentials:"
Write-Host "Username: sqladmin"
if ($CreateParameters) {
    Write-Host "Password: $plainTextPassword"
    Write-Host "`nNOTE: Please store this password securely as it won't be shown again."
} else {
    Write-Host "Password: [Stored in Key Vault or parameters file]"
}
