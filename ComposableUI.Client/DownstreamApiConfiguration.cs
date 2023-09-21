namespace ComposableUI.Client;

public class DownstreamApiConfiguration
{
    public required Uri BaseUrl { get; init; }
    public required string[] Scopes { get; init; }
}