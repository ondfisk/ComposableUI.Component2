using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace ComposableUI.Client;

public class DownstreamApiAuthorizationMessageHandler : AuthorizationMessageHandler
{
    public DownstreamApiAuthorizationMessageHandler(IAccessTokenProvider provider,
        NavigationManager navigation, IConfiguration config)
        : base(provider, navigation)
    {
        ConfigureHandler(
            authorizedUrls: new[] { config.GetSection("DownstreamApi")["BaseUrl"] },
            scopes: config.GetSection("DownstreamApi:Scopes").Get<List<string>>());
    }
}
