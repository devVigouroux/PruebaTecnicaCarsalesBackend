using Xunit;
using FluentAssertions;
using PruebaTecnicaCarsales.BFF.Services;
using PruebaTecnicaCarsales.BFF.Dto;
using Microsoft.Extensions.Logging.Abstractions;
using PruebaTecnicaCarsales.BFF.Exceptions;

namespace PruebaTecnicaCarsales.Test;

public class ContactoServiceTests
{
    [Fact]
    public void Create_Contact()
    {
        var service = new ContactoService(NullLogger<ContactoService>.Instance);

        var dto = new ContactoDto
        {
            Nombre = "Simon",
            Telefono = "987654321"
        };

        var result = service.Create(dto);

        result.Should().NotBeNull();
        result.Id.Should().BeGreaterThan(0);
        result.Nombre.Should().Be("Simon");
        result.Telefono.Should().Be("987654321");
    }


    [Fact]
    public void Update_Contact()
    {
        var service = new ContactoService(NullLogger<ContactoService>.Instance);

        var dto = new ContactoDto
        {
            Nombre = "Isaias",
            Telefono = "950463036"
        };

        var created = service.Create(dto);

        var updateDto = new ContactoDto
        {
            Nombre = "Isaias Pereira",
            Telefono = "950461111"
        };

        var updated = service.Update(created.Id, updateDto);

        updated.Nombre.Should().Be("Isaias Pereira");
        updated.Telefono.Should().Be("950461111");
    }

    
    [Fact]
    public void ValidateDuplicatePhoneContact()
    {
        var service = new ContactoService(
            NullLogger<ContactoService>.Instance);

        var dto = new ContactoDto
        {
            Nombre = "Juan",
            Telefono = "955555555"
        };

        service.Create(dto);

        var duplicateDto = new ContactoDto
        {
            Nombre = "Pedro",
            Telefono = "955555555"
        };

        Action act = () => service.Create(duplicateDto);

        act.Should().Throw<ConflictException>();
    }
}