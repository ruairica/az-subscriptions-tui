using Terminal.Gui;

namespace Az.Subscriptions;

public class SubscriptionsWindow : Window
{
    public SubscriptionsWindow()
    {
        ColorScheme = Colors.Menu; 
        var azureAccount = File.ReadAllText(SubscriptionsListView.PATH).Deserialize<AzureAccount>();
        Title = azureAccount is null
            ? $"Azure config not found at {SubscriptionsListView.PATH}"
            : azureAccount.Subscriptions.Count == 0
                ? "No subscriptions found, ensure you have logged in with 'az login'"
                : "Azure subscriptions (↑ / ↓  to move, <space> to select, q to quit)";

        if (azureAccount is not null)
        {
            base.Add(new SubscriptionsListView(azureAccount));
        }
    }
}