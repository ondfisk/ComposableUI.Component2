using ComposableUI.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<DownstreamApiAuthorizationMessageHandler>();
builder.Services.AddHttpClient("ComposableUI.DownstreamApi", client => client.BaseAddress = new Uri(builder.Configuration.GetSection("DownstreamApi")["BaseUrl"]))
    .AddHttpMessageHandler<DownstreamApiAuthorizationMessageHandler>();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ComposableUI.DownstreamApi"));

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
    foreach (var scope in builder.Configuration.GetSection("DownstreamApi:Scopes").Get<List<string>>())
    {
        options.ProviderOptions.DefaultAccessTokenScopes.Add(scope);
    }   
});

await builder.Build().RunAsync();
