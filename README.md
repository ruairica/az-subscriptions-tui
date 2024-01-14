# az-subscriptions-tui
A simple terminal user interface (TUI) to easily switch between subscriptions with the Azure CLI. Displays all subscriptions and lets you switch between which one will be used by default with azure cli commands.

Saves you having to ```az account subscription list```, then copying the subscription name then doing ```az account set --subscription "subscriptionName"```

#### Installation steps:
1. Clone this repository
2. Build the project. Run ```dotnet build --configuration Release``` in the ```Az.Subscriptions``` folder
3. (Powershell specific) Create an alias to to the application, run ```code $PROFILE``` to open your powershell profile in VS code. Add an alias that can be used to start the application eg. ```Set-Alias subs "insert\full\path\to\project\Az.Subscriptions\bin\Release\net8.0\Az.Subscriptions.exe"```
4. Type ```subs``` in powershell to run the TUI.
#### TODO
* Make an actual release version so it can be installed.