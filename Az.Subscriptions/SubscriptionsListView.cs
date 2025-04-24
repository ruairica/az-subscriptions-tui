using Terminal.Gui;
using System.Runtime.InteropServices;

namespace Az.Subscriptions;

public class SubscriptionsListView : ListView
{
    public static string PATH => Path.Combine(
            Environment.GetFolderPath(RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                                                ? Environment.SpecialFolder.UserProfile
                                                : Environment.SpecialFolder.Personal),
        ".azure",
        "azureProfile.json");

    private readonly AzureAccount _azureAccount;

    public SubscriptionsListView(AzureAccount azureAccount)
        : base(azureAccount.Subscriptions.Select(s => s.Name).ToList())
    {
        _azureAccount = azureAccount;
        Height = Dim.Fill();
        Width = Dim.Fill();
        AllowsMarking = true;
        AllowsMultipleSelection = false;
        SelectedItem = _azureAccount.Subscriptions.FindIndex(x => x.IsDefault);
        MarkUnmarkRow();

        KeyPress += args => {
            if (args.KeyEvent.Key is Key.q)
            {
                Application.RequestStop();
            }
        };
    }

    public override bool MarkUnmarkRow()
    {
        if (Source.IsMarked(SelectedItem))
        {
            return false;
        }

        AllowsAll();
        File.WriteAllText(PATH, CreateWithActiveSubscription(_azureAccount, SelectedItem).Serialize());
        Source.SetMark(SelectedItem, true);
        SetNeedsDisplay();
        return true;
    }

    private static AzureAccount CreateWithActiveSubscription(AzureAccount azureAccount, int active) =>
        new AzureAccount(azureAccount.InstallationId,
            [..azureAccount
                .Subscriptions
                .Select((s, index) => s with { IsDefault = index == active })]
                
        );
}
