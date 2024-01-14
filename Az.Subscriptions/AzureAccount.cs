namespace Az.Subscriptions;


public record AzureAccount(string InstallationId, Subscription[] Subscriptions);

public record Subscription(
    string Id,
    string Name,
    string State,
    User User,
    bool IsDefault,
    string TenantId,
    string EnvironmentName,
    string HomeTenantId,
    Managedbytenant[] ManagedByTenants
);

public record User(string Name, string Type);

public record Managedbytenant(string TenantId);