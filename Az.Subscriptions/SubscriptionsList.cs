using Terminal.Gui;
using System.Text.Json;
using System.Runtime.InteropServices;

namespace Az.Subscriptions;

public class SubscriptionsList : ListView
{
    public static string PATH => Path.Combine(
            Environment.GetFolderPath(RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                                                ? Environment.SpecialFolder.UserProfile
                                                : Environment.SpecialFolder.Personal),
        ".azure",
        "azureProfile.json");

    public static readonly JsonSerializerOptions JSON_OPTIONS = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    private readonly AzureAccount azureAccount;

    public SubscriptionsList(AzureAccount azureAccount)
        : base(azureAccount.Subscriptions.Select(s => s.Name).ToList())
    {
        this.azureAccount = azureAccount;
        Height = Dim.Fill();
        Width = Dim.Fill();
        AllowsMarking = true;
        AllowsMultipleSelection = false;
        SelectedItem = this.azureAccount.Subscriptions.ToList().FindIndex(x => x.IsDefault);
        MarkUnmarkRow();
    }

    public override bool MarkUnmarkRow()
    {
        if (Source.IsMarked(SelectedItem))
        {
            return false;
        }

        AllowsAll();
        File.WriteAllText(PATH, Serialize(ToUpdatedAzureAccount(azureAccount, SelectedItem)));
        Source.SetMark(SelectedItem, true);
        SetNeedsDisplay();
        return true;
    }

    private static AzureAccount ToUpdatedAzureAccount(AzureAccount azureAccount, int active) =>
        new(azureAccount.InstallationId,
            azureAccount
                .Subscriptions
                .Select((s, index) => s with { IsDefault = index == active })
                .ToArray()
        );

    private static string Serialize(AzureAccount obj) =>
        JsonSerializer.Serialize(obj, JSON_OPTIONS);
}
