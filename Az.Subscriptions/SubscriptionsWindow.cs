using System.Text.Json;
using Terminal.Gui;

namespace Az.Subscriptions;

public class SubscriptionsWindow : Window
{
    public SubscriptionsWindow()
    {
        ColorScheme = Colors.Menu;
        Title = "Azure subscriptions (↑ / ↓  to move, <space> to select, q to quit)";

        Add(new SubscriptionsList(Deserialize(File.ReadAllText(SubscriptionsList.PATH))));
    }


    private static AzureAccount Deserialize(string text) =>
        JsonSerializer.Deserialize<AzureAccount>(text, SubscriptionsList.JSON_OPTIONS) ?? throw new Exception("didn't parse json");
}