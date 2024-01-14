using System.Text.Json;
using Terminal.Gui;

namespace Az.Subscriptions;

public class SubscriptionsWindow : Window
{
    public SubscriptionsWindow()
    {
        ColorScheme = Colors.Menu;
        AzureAccount? azureAccount = Deserialize(File.ReadAllText(SubscriptionsList.PATH));
        Title = azureAccount is null
            ? $"Azure config not found at {SubscriptionsList.PATH}"
            : azureAccount.Subscriptions.Length == 0
                ? $"No subscriptions found, ensure you have logged in with 'az login'"
                : "Azure subscriptions (↑ / ↓  to move, <space> to select, q to quit)";

        if (azureAccount is not null)
        {
            base.Add(new SubscriptionsList(azureAccount));
        }
    }

    private static AzureAccount? Deserialize(string text) =>
        JsonSerializer.Deserialize<AzureAccount>(text, SubscriptionsList.JSON_OPTIONS);
}