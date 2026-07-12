namespace ContactApi;

/// <summary>
/// Central list of all feature flag names used in the application.
/// Each flag can be toggled on/off in appsettings.json.
/// </summary>
public static class FeatureFlags
{
    /// <summary>
    /// When enabled, adds PUT /contacts/{id} endpoint for updating existing contacts.
    /// When disabled, only Create, Read, and Delete are available.
    /// </summary>
    public const string EnableUpdateEndpoint = "EnableUpdateEndpoint";

    /// <summary>
    /// When enabled, adds GET /contacts/search?q= endpoint for searching contacts by name.
    /// When disabled, only the basic list-all endpoint is available.
    /// </summary>
    public const string EnableSearchEndpoint = "EnableSearchEndpoint";

    /// <summary>
    /// When enabled, v2 API endpoints are available at /v2/contacts/.
    /// When disabled, only v1 endpoints respond.
    /// </summary>
    public const string EnableV2Api = "EnableV2Api";
}