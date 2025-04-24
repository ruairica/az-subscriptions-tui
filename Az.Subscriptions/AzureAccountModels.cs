namespace Az.Subscriptions;

public class AzureAccount
{
    public string InstallationId { get; set; } = null!;
    public List<Subscription> Subscriptions { get; set; } = [];

    public void SetActiveSubscription(int activeIndex)
    {
        foreach (var (subscription, index) in Subscriptions.Select((e, i) => (e, i)))
        {
            subscription.IsDefault = index == activeIndex;
        }
    }
}

public class Subscription
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string State { get; set; } = null!;
    public User? User { get; set; }
    public bool IsDefault { get; set; }
    public string TenantId { get; set; } = null!;
    public string EnvironmentName { get; set; } = null!;
    public string HomeTenantId { get; set; } = null!;
    public List<ManagedByTenant> ManagedByTenants { get; set; } = [];
}

public class User
{
    public string Name { get; set; } = null!;
    public string Type { get; set; } = null!;
}

public class ManagedByTenant
{
    public string TenantId { get; set; } = null!;
}