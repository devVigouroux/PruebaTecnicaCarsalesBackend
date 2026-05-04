using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using PruebaTecnicaCarsales.BFF.Dto;
using Xunit;

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
        // Genera teléfono único para evitar conflictos
        var telefono = Random.Shared
            .Next(900000000, 999999999)
            .ToString();

        var dto = new ContactoDto
        {
            Nombre = "Integracion",
            Telefono = telefono
        };

        var response = await _client
            .PostAsJsonAsync("/api/Contacto", dto);

        response.StatusCode
            .Should()
            .Be(HttpStatusCode.Created);

        var content = await response
            .Content
            .ReadAsStringAsync();

        content.Should().Contain("Integracion");
        content.Should().Contain(telefono);
    }
}