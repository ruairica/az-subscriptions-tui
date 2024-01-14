using Terminal.Gui;
using System.Collections;
using System.Text.Json;

namespace Az.Subscriptions;

public class CustomList : ListView
{
    private readonly AzureAccount azureAccount;

    public CustomList(AzureAccount azureAccount) : base(azureAccount.subscriptions.Select(s => s.name).ToList())
    {
        this.azureAccount = azureAccount;
    }

    public override bool MarkUnmarkRow()
    {
        if (Source.IsMarked(SelectedItem))
        {
            return false;
        }

        AllowsAll();
        File.WriteAllText(ExampleWindow.Path, JsonSerializer.Serialize(ToUpdatedAzureAccount(azureAccount, this.SelectedItem)));
        Source.SetMark(SelectedItem, true);
        SetNeedsDisplay();
        return true;
    }


    public static AzureAccount ToUpdatedAzureAccount(AzureAccount azureAccount, int active)
    {
        return new AzureAccount
        {
            installationId = azureAccount.installationId,
            subscriptions = azureAccount.subscriptions.Select((s, index) => new Subscription
            {
                id = s.id,
                name = s.name,
                state = s.state,
                user = new User { name = s.user.name, type = s.user.type },
                isDefault = index == active,
                tenantId = s.tenantId,
                environmentName = s.environmentName,
                homeTenantId = s.homeTenantId,
                managedByTenants = s.managedByTenants.Select(m => new Managedbytenant { tenantId = m.tenantId }).ToArray()
            }).ToArray(),
        };
    }
}
