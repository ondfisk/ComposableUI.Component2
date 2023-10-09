namespace ComposableUI.Component2.Tests;

public static class TestExtensions
{
    public static MockHttpMessageHandler AddTestHttpClient(this TestServiceProvider services)
    {
        var handler = new MockHttpMessageHandler();
        var client = handler.ToHttpClient();
        client.BaseAddress = new Uri("https://localhost");
        services.AddSingleton(client);
        return handler;
    }

    public static MockedRequest RespondJson<T>(this MockedRequest request, T content)
    {
        request.Respond(req =>
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(content))
                {
                    Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
                }
            };
        });
        return request;
    }

    public static MockedRequest RespondJson<T>(this MockedRequest request, Func<T> contentProvider)
    {
        request.Respond(req =>
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(contentProvider()))
                {
                    Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
                }
            };
        });
        return request;
    }
}