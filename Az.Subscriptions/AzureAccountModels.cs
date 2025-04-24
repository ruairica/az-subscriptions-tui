namespace Az.Subscriptions;

public record AzureAccount(string InstallationId, List<Subscription> Subscriptions);

public record Subscription(
    string Id,
    string Name,
    string State,
    User User,
    bool IsDefault,
    string TenantId,
    string EnvironmentName,
    string HomeTenantId,
    List<ManagedByTenant> ManagedByTenants
);

public record User(string Name, string Type);

public record ManagedByTenant(string TenantId);