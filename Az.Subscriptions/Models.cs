namespace Az.Subscriptions;


public class AzureAccount
{
    public string installationId { get; set; }
    public Subscription[] subscriptions { get; set; }
}

public class Subscription
{
    public string id { get; set; }
    public string name { get; set; }
    public string state { get; set; }
    public User user { get; set; }
    public bool isDefault { get; set; }
    public string tenantId { get; set; }
    public string environmentName { get; set; }
    public string homeTenantId { get; set; }
    public Managedbytenant[] managedByTenants { get; set; }
}

public class User
{
    public string name { get; set; }
    public string type { get; set; }
}

public class Managedbytenant
{
    public string tenantId { get; set; }
}
