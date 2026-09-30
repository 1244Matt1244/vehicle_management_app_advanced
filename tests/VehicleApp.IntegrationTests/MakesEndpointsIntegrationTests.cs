using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace VehicleApp.IntegrationTests;

public class MakesEndpointsIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public MakesEndpointsIntegrationTests(CustomWebApplicationFactory factory)
        => _client = factory.CreateClient();

    [Fact]
    public async Task CreateMake_WithEmptyName_Returns400()
    {
        var body = new { name = "", abrv = "TM" };
        var response = await _client.PostAsJsonAsync("/api/makes", body);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("Validation failed");
        content.Should().Contain("Name");
    }

    [Fact]
    public async Task CreateMake_WithEmptyAbrv_Returns400()
    {
        var body = new { name = "Test Make", abrv = "" };
        var response = await _client.PostAsJsonAsync("/api/makes", body);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("Validation failed");
        content.Should().Contain("Abbreviation");
    }

    [Fact]
    public async Task CreateMake_WithValidData_ButNoDb_Returns500()
    {
        // Ovo testira da je validacija prošla, ali DB nije dostupan (Fake factory baca exception)
        // Očekujemo 500 jer nema prave baze.
        var body = new { name = "Test Make", abrv = "TM" };
        var response = await _client.PostAsJsonAsync("/api/makes", body);

        // Pošto FakeDbConnectionFactory baca NotImplementedException, očekujemo 500
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task GetMakes_WithoutDb_Returns500()
    {
        var response = await _client.GetAsync("/api/makes");
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }
}
