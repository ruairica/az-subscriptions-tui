using System.Text.Json;

namespace Az.Subscriptions;

public static class Serialization
{
    private static readonly JsonSerializerOptions JSON_OPTIONS = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public static T? Deserialize<T>(this string text) where T : class => JsonSerializer.Deserialize<T>(text, JSON_OPTIONS);

    public static string Serialize(this object text) => JsonSerializer.Serialize(text, JSON_OPTIONS);
}