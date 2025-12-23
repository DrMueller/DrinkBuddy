terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = ">= 3.0.0"
    }
  }
  # The values are needed for local commands like creating the workspaces
  backend "azurerm" {
    resource_group_name  = "TerraformStuff"
    storage_account_name = "terraformstuffblob"
    container_name       = "terraformstuffblobcontainer"
    key                  = "tf/terraform.tfstate"
  }
}

provider "azurerm" {
  subscription_id = var.subscription_id
  tenant_id       = "d6fddda6-f690-4755-92c2-f22a3521bab0"
  features {
  }
}

resource "azurerm_windows_web_app" "app" {
  name                = "DrinkBuddy"
  resource_group_name = "Matthias"
  location            = "westeurope"
  service_plan_id     = "/subscriptions/91660754-3529-407f-8458-92759935fbf7/resourceGroups/matthias/providers/Microsoft.Web/serverFarms/ASP-Matthias-9263"

  site_config {
    always_on = false
  }
}