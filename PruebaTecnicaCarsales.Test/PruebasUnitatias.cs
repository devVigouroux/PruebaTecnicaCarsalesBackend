using Xunit;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;
using PruebaTecnicaCarsales.BFF.Services;
using PruebaTecnicaCarsales.BFF.Dto;

namespace PruebaTecnicaCarsales.Test;

public class ContactServiceTests
{
    [Fact]
    public void Create_Should_Add_Contact_With_AutoIncrement_Id()
    {
        var service = new ContactService();

        var dto = new ContactDto
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
    public void ContactDto_Should_Be_Invalid_When_Telefono_Has_More_Than_9_Digits()
    {
        var dto = new ContactDto
        {
            Nombre = "Simon",
            Telefono = "98765432122222222222222"
        };

        var context = new ValidationContext(dto);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(dto, context, results, true);

        isValid.Should().BeFalse();
    }
    [Fact]
    public void Update_Should_Modify_Contact()
    {
        var service = new ContactService();

        var dto = new ContactDto
        {
            Nombre = "Simon",
            Telefono = "987654321"
        };

        var created = service.Create(dto);

        var updateDto = new ContactDto
        {
            Nombre = "Simon Updated",
            Telefono = "912345678"
        };

        var updated = service.Update(created.Id, updateDto);

        updated.Nombre.Should().Be("Simon Updated");
        updated.Telefono.Should().Be("912345678");
    }
}