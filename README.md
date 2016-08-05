---
services: app-service, functions
platforms: nodejs
author: christopheranderson
---

# Generating exponential load on Azure Functions & Storage Queues

This is a fairly simple example of generating exponential load on Azure Functions & Storage Queues. 

##Deploy to Azure

The automated deployment provisions an Azure Storage account and an Azure Function in a Dynamic compute plan and sets up deployment from source control. 

The deployment template has a parameter `manualIntegration` which controls whether or not a deployment trigger is registered with GitHub. Use `true` if you are deploying from the main Azure-Samples repo (does not register hook), `false` otherwise (registers hook). Since a value of `false` registers the deployment hook with GitHub, deployment will fail if you don't have write permissions to the repo.

## How it works



##Calling the function



## Learn more


