param appName string
param location string = resourceGroup().location

resource appServicePlan 'Microsoft.Web/serverfarms@2023-12-01' = {
  name: 'BPPlan'
  location: location
  sku: {
    name: 'F1'
    tier: 'Free'
    size: 'F1'
    capacity: 1
  }
  properties: {
    reserved: false
  }
}

resource webApp 'Microsoft.Web/sites@2023-12-01' = {
  name: appName
  location: location
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: {
      netFrameworkVersion: 'v8.0'
      windowsFxVersion: null
      linuxFxVersion: null
    }
  }
}

output webAppName string = webApp.name
output appServicePlanName string = appServicePlan.name

