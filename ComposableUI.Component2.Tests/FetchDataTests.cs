namespace ComposableUI.Component2.Tests;

public class FetchDataTests
{
    [Fact]
    public void PageShouldLoadWeatherForecast()
    {
        // Arrange
        using var ctx = new TestContext();
        var fakeHttp = ctx.Services.AddTestHttpClient();
        var forecasts = new List<WeatherForecast>
        {
            new(DateOnly.FromDateTime(DateTime.Now), 20, "Warm" ),
            new(DateOnly.FromDateTime(DateTime.Now.AddDays(1)), 15, "Cool" ),
            new(DateOnly.FromDateTime(DateTime.Now.AddDays(2)),  25, "Hot" )
        };
        fakeHttp.When("https://localhost/WeatherForecast").RespondJson(forecasts);

        // Act
        var cut = ctx.RenderComponent<FetchData>();

        // Assert
        var tbodyElm = cut.WaitForElement("table > tbody");
        tbodyElm.ChildElementCount.Should().Be(forecasts.Count);
    }
}
