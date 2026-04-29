using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using PruebaTecnicaCarsales.BFF.Dto;

namespace PruebaTecnicaCarsales.Test;

public class PruebasIntegracion : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public PruebasIntegracion(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task PostContact_Should_Create_Contact()
    {
        var dto = new ContactDto
        {
            Nombre = "Integracion",
            Telefono = "987654321"
        };

        var response = await _client.PostAsJsonAsync("/api/Contacts", dto);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("Integracion");
        content.Should().Contain("987654321");
    }
}